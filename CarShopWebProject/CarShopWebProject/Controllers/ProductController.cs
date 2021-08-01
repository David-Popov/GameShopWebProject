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
            ViewBag.Platforms = productService.GetProductPlatforms();

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
                product.CategoryId,
                product.PlatformId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Platforms(string id, string searchTerm)
        {
            var productQuerry = db.Product.AsQueryable();


            if (id == null)
            {
                return BadRequest();
            }

            var products = productService.GetProductsByPlatformId(id);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuerry = productQuerry.Where(x =>
                x.Tittle.ToLower().Contains(searchTerm.ToLower()) ||
                x.Company.ToLower().Contains(searchTerm.ToLower()));

                 products = productQuerry
                .OrderByDescending(c => c.Id)
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
            }

            if (products == null) return NotFound();

            var viewmodel = new PlatformGamesViewModel();

            viewmodel.Products = products;

            ViewBag.Platforms = productService.GetProductPlatforms();


            return View(viewmodel);
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
