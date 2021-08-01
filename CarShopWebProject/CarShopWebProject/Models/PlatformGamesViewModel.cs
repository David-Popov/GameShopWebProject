using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShopWebProject.Models
{
    public class PlatformGamesViewModel
    {
        public IEnumerable<string> Tittles { get; set; }
        [Display(Name ="Search")]
        public string SearchTerm { get; set; }

        public ProductSorting Sorting { get; set; }
        public IEnumerable<ProductFormModel> Products { get; set; } = new List<ProductFormModel>();
    }
}