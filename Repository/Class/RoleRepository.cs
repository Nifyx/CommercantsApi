using CommercantsAPI.Models;
using CommercantsAPI.Models.Roles;
using CommercantsAPI.Services;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommercantsAPI.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(Context context):base(context)
        {
        }
    }
}
