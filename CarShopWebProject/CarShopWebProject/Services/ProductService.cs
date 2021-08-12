using CarShopWebProject.Data;
using CarShopWebProject.Data.Models;
using CarShopWebProject.Models;
using Microsoft.AspNetCore.Mvc;
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

        public void AddGiftCard(string title, int price, string description, string imageUrl, string platformId)
        {
            var addGiftCard = new GiftCards
            {
                Tittle = title,
                Price = price,
                Description = description,
                ImageUrl = imageUrl,
                PlatformId = platformId
            };

            db.GiftCards.Add(addGiftCard);
            db.SaveChanges();
        }

        public void AddProduct(string title, int price, int year, string description, string imageUrl, string company, string categoryId, string platformId)
        {
            var addProduct = new Product
            {
                Tittle = title,
                Price = price,
                Year = year,
                Description = description,
                ImageUrl = imageUrl,
                Company = company,
                CategoryId = categoryId,
                PlatformId = platformId
            };

            db.Product.Add(addProduct);
            db.SaveChanges();
        }

        public IEnumerable<CategoryFormModel> GetCategories()
        => db.Category
                .Select(x => new CategoryFormModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

        public IEnumerable<Product> GetDbProduct(string id)
         => db.Product
            .Where(x => x.Id.ToString() == id)
            .Select(c => new Product
            {
                Tittle = c.Tittle,
                Price = c.Price,
                Year = c.Year,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Company = c.Company,
                CategoryId = c.CategoryId,
                PlatformId = c.PlatformId
            }).ToList();

        public IEnumerable<GiftCardFormModel> GetGiftCardByPlatformId(string id, AllGamesQueryModel query)
        => db.GiftCards
            .Where(x => x.PlatformId == id)
            .Skip((query.CurrentPage) * AllGamesQueryModel.GamesPerPage)
            .Take(AllGamesQueryModel.GamesPerPage)
            .Select(c => new GiftCardFormModel
            {
                Tittle = c.Tittle,
                Price = c.Price,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                PlatformId = c.PlatformId
            }).ToList();

        public IEnumerable<ProductFormModel> GetProduct()
        => db.Product
            .Select(c => new ProductFormModel
            {
                Tittle = c.Tittle,
                Price = c.Price,
                Year = c.Year,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Company = c.Company,
                CategoryId = c.CategoryId,
                PlatformId = c.PlatformId
            }).ToList();

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

        public IEnumerable<DeleteProductModel> GetProducts()
        {
            var products = db.Product
                .OrderBy(x => x.Year)
                .Select(c => new DeleteProductModel
                {
                    Id = c.Id.ToString(),
                    Tittle = c.Tittle,
                    Company = c.Company,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    CategoryId = c.CategoryId,
                    PlatformId = c.PlatformId,
                    Price = c.Price,
                    Year = c.Year
                })
                .ToList();

            return products;
        }

        public IEnumerable<ProductFormModel> GetProductsByPlatformId(string id, AllGamesQueryModel query)
        => db.Product
            .Where(x => x.PlatformId == id)
            .Skip((query.CurrentPage) * AllGamesQueryModel.GamesPerPage)
            .Take(AllGamesQueryModel.GamesPerPage)
            .Select(c => new ProductFormModel
            {
                Tittle = c.Tittle,
                Price = c.Price,
                Year = c.Year,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Company = c.Company,
                CategoryId = c.CategoryId,
                PlatformId = c.PlatformId
            }).ToList();

        public IEnumerable<ProductFormModel> SelectByCategory(string id, [FromQuery] AllGamesQueryModel query)
        =>
            db.Product.AsQueryable()
                        .Where(c => c.CategoryId == query.SelectedCategory && c.PlatformId == id)
                        .Select(x => new ProductFormModel
                        {
                            Tittle = x.Tittle,
                            Price = x.Price,
                            Year = x.Year,
                            Description = x.Description,
                            ImageUrl = x.ImageUrl,
                            Company = x.Company,
                            CategoryId = x.CategoryId,
                            PlatformId = x.PlatformId
                        }).ToList();

        public IEnumerable<ProductFormModel> SelectBySearchTerm([FromQuery] AllGamesQueryModel query)
        { var productQuerry = db.Product.AsQueryable();


            productQuerry.Where(x =>
               x.Tittle.ToLower().Contains(query.SearchTerm.ToLower()) ||
               x.Company.ToLower().Contains(query.SearchTerm.ToLower()));

            var product = productQuerry.OrderByDescending(c => c.Id)
           .Select(x => new ProductFormModel
           {
               Tittle = x.Tittle,
               Price = x.Price,
               Year = x.Year,
               Description = x.Description,
               ImageUrl = x.ImageUrl,
               Company = x.Company,
               CategoryId = x.CategoryId,
               PlatformId = x.PlatformId
           }).ToList();

            return product;
        }
    }
}
