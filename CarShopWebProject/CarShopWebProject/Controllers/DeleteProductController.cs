using CarShopWebProject.Data;
using CarShopWebProject.Models;
using CarShopWebProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Controllers
{
    public class DeleteProductController :Controller
    {
        private readonly GameShopDbContext db;
        private readonly IProductService productService;

        public DeleteProductController(GameShopDbContext db, IProductService productService)
        {
            this.db = db;
            this.productService = productService;
        }

        public IActionResult Delete()
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

            return View(products);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var product = db.Product
                .Where(x => x.Id.ToString() == id)
                .FirstOrDefault();



            db.Product.Remove(product);
            db.SaveChanges();

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

            

            return View(products);
        }
    }
}
