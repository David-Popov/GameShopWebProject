using CarShopWebProject.Data;
using CarShopWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Services
{
    public class ProductService : IProductService
    {
        private readonly GameShopDbContext db;

        public ProductService(GameShopDbContext db)
        {
            this.db = db;
        }

        public void AddProduct(string title, int price, int year, string description, string imageUrl, string company, string categoryId)
        {
            var addProduct = new Product
            {
                Tittle = title,
                Price = price,
                Year = year,
                Description = description,
                ImageUrl = imageUrl,
                Company = company,
                CategoryId = categoryId
            };

            db.Product.Add(addProduct);
            db.SaveChanges();
        }

        public IEnumerable<CategoryFormModel> GetProductCategories()
        => db.Category
               .Select(x => new CategoryFormModel
               {
                   Id = x.Id,
                   Name = x.Name
               }).ToList();

        public IEnumerable<PlatformFormModel> GetProductPlatforms()
           => db.Platform
               .Select(x => new PlatformFormModel
               {
                   Id = x.Id,
                   Name = x.Name
               }).ToList();
        
    }
}
