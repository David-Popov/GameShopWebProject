using CarShopWebProject.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShopWebProject.Models
{
    public class AllGamesQueryModel
    {
        public const int GamesPerPage = 6;

        public int CurrentPage { get; set; } 
        [Display(Name ="Selected Category")]
        public string SelectedCategory { get; set; }
        public IEnumerable<CategoryFormModel> Categories { get; set; } = new List<CategoryFormModel>();
        [Display(Name ="Search")]
        public string SearchTerm { get; set; }

        public ProductSorting Sorting { get; set; }
        public IEnumerable<ProductFormModel> Products { get; set; } = new List<ProductFormModel>();
        public IEnumerable<GiftCardFormModel> GiftCards { get; set; } = new List<GiftCardFormModel>();
    }
}