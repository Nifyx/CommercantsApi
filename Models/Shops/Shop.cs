using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommercantsAPI.Models.Shops
{
    [Table("Shop")]
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Street { get; set; }
        [Required]
        [StringLength(5)]
        public string PostalCode { get; set; }
        public string ImagePath { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
