using CommercantsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommercantsAPI.Repository
{
    public interface IRepositoryWrapper
    {
        IShopRepository Shop { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }
    }
}
