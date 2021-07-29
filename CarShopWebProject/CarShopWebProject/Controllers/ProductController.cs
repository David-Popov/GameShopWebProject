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
    public class ProductController : Controller
    {
        private readonly GameShopDbContext db;
        private readonly IProductService productService;

        public ProductController(GameShopDbContext db, IProductService productService)
        {
            this.db = db;
            this.productService = productService;
        }
        public IActionResult Add()
        {
            return View(new ProductFormModel { Categories = productService.GetProductCategories(),Platforms = productService.GetProductPlatforms() });
        }

        [HttpPost]
        public IActionResult Add(ProductFormModel product)
        {
            productService.AddProduct(
                product.Tittle,
                product.Price,
                product.Year,
                product.Description,
                product.ImageUrl,
                product.Company,
                product.CategoryId);

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<CategoryFormModel> GetProductCategories()
         => db.Category
               .Select(x => new CategoryFormModel
               {
                   Id = x.Id,
                   Name = x.Name
               }).ToList();

        private IEnumerable<PlatformFormModel> GetProductPlatforms()
        => db.Platform
                .Select(x => new PlatformFormModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
    }
}
