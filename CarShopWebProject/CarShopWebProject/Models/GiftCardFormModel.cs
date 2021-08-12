using CarShopWebProject.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CarShopWebProject.Models
{
    public class GiftCardFormModel
    {
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

        public IEnumerable<PlatformFormModel> Platforms { get; set; }
    }
}
