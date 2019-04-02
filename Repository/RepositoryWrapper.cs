using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommercantsAPI.Services;
using Entities.Models;

namespace CommercantsAPI.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private Context _context;
        private IShopRepository _shop;
        private IRoleRepository _role;
        private IUserRepository _user;

        public IShopRepository Shop
        {
            get
            {
                if(_shop == null)
                {
                    _shop = new ShopRepository(_context);
                }
                return _shop;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if(_role == null)
                {
                    _role = new RoleRepository(_context);
                }
                return _role;
            }
        }

        public IUserRepository User
        {
            get
            {
                if(_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        public RepositoryWrapper(Context context)
        {
            _context = context;
        }
    }
}
