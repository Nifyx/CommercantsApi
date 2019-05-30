using CommercantsAPI.Exceptions;
using CommercantsAPI.Models;
using CommercantsAPI.Models.Users;
using CommercantsAPI.Services;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CommercantsAPI.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(Context context):base(context)
        {
        }

        public User Authenticate(string mail, string password)
        {
            User user = null;
            if(!String.IsNullOrEmpty(mail) && !String.IsNullOrEmpty(password))
            {
                user = this.Context.Users.SingleOrDefault(x => x.Mail == mail);
                if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    user = null;
                }
            }
            return user;
        }

        public User Create(User user, string password)
        {
            if (String.IsNullOrEmpty(password)) throw new AppException("Password is required");
            if (this.Context.Users.Any(x => x.Mail == user.Mail)) throw new AppException("Mail \"" + user.Mail + " is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = this.Context.Roles.Find(2);

            this.Context.Users.Add(user);
            this.Context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            User user = null;
            user = this.Context.Users.Find(userParam.Id);
            
            if(user != null)
            {
                if(userParam.Mail != user.Mail)
                {
                    if (this.Context.Users.Any(x => x.Mail == userParam.Mail)) throw new AppException("Mail \"" + userParam.Mail + " is already taken.");
                }

                user.Name = userParam.Name;
                user.Surname = userParam.Surname;
                user.Mail = userParam.Mail;

                if (!String.IsNullOrEmpty(password))
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                this.Context.Users.Update(user);
                this.Context.SaveChanges();
            }
            else
            {
                throw new AppException("User not found");
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordhash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordsalt");

            using (HMACSHA512 hmac = new HMACSHA512(storedSalt))
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
