using CarShopWebProject.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Data
{
    public class Product
    {
        public int Id { get; set; }
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
        [Required]
        public string PlatformId { get; set; }
        public Platform Platform { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
