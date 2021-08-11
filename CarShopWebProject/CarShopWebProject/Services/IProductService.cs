using CarShopWebProject.Data;
using CarShopWebProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Services
{
    public interface IProductService
    {
        public IEnumerable<CategoryFormModel> GetProductCategories();

        public IEnumerable<Product> GetDbProduct(string id);

        public IEnumerable<DeleteProductModel> GetProducts();

        public IEnumerable<ProductFormModel> GetProduct();
        public IEnumerable<ProductFormModel> GetProductsByPlatformId(string id,AllGamesQueryModel query);
        public IEnumerable<PlatformFormModel> GetProductPlatforms();

        public IEnumerable<ProductFormModel> SelectByCategory(string id, [FromQuery] AllGamesQueryModel query);

        public IEnumerable<ProductFormModel> SelectBySearchTerm([FromQuery] AllGamesQueryModel query);

        public IEnumerable<CategoryFormModel> GetCategories();

        void AddProduct(string title, int price, int year, string description,string imageUrl,string company,string categoryId,string platformId);

       
    }
}
