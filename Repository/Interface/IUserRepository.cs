using CommercantsAPI.Models.Users;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommercantsAPI.Services
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User Authenticate(string mail, string password);
        User Create(User user, string password);
        void Update(User user, string password);
    }
}
