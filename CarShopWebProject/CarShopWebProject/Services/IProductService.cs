using CarShopWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Services
{
    public interface IProductService
    {
        public IEnumerable<CategoryFormModel> GetProductCategories();

        public IEnumerable<PlatformFormModel> GetProductPlatforms();

        void AddProduct(string title, int price, int year, string description,string imageUrl,string company,string categoryId);

        //Tittle = game.Tittle,
        //     Price = game.Price,
        //     Year = game.Year,
        //     Description = game.Description,
        //     ImageUrl = game.ImageUrl,
        //     Company = game.Company,
        //     CategoryId = game.CategoryId
    }
}
