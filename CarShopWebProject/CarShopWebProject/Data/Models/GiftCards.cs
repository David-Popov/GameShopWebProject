using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Data.Models
{
    public class GiftCards
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Tittle { get; set; }
        [Required]
        [Range(15, 180)]
        public int Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        public string PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
