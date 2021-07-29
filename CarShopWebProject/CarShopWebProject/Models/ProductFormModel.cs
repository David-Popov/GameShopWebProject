using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Models
{
    public class ProductFormModel
    {
        [Required]
        [MaxLength(40)]
        public string Tittle { get; set; }
        [Required]
        [Range(15, 180)]
        public int Price { get; set; }
        [Required]
        [Range(2005, 2100)]
        public int Year { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(20)]
        public string Company { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        public string PlatformId { get; set; }
        [Required]
        public string CategoryId { get; set; }

        public IEnumerable<PlatformFormModel> Platforms { get; set; } 
        public IEnumerable<CategoryFormModel> Categories { get; set; }
    }
}
