using CommercantsAPI.Models.Roles;
using CommercantsAPI.Models.Shops;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommercantsAPI.Models.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string password { get; set; }

        //public DateTime? Created_at { get; set; }
        //public DateTime? Updated_at { get; set; }

        //public Role Role { get; set; }
        //public Shop Shop { get; set; }
    }
}
