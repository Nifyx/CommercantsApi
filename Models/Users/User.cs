using CommercantsAPI.Models.Roles;
using CommercantsAPI.Models.Shops;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommercantsAPI.Models.Users
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Mail { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        [Required]
        public Role Role { get; set; }
        public Shop Shop { get; set; }
    }
}
