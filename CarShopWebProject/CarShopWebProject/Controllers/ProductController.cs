using CarShopWebProject.Data;
using CarShopWebProject.Models;
using CarShopWebProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public IActionResult Platforms(string id, [FromQuery]AllGamesQueryModel query)
        {
            var productQuerry = db.Product.AsQueryable();


            if (id == null)
            {
                return BadRequest();
            }

            var products = productService.GetProductsByPlatformId(id,query);

            if (!string.IsNullOrEmpty(query.SelectedCategory))
            {
                products = productQuerry
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
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productQuerry = productQuerry.Where(x =>
                x.Tittle.ToLower().Contains(query.SearchTerm.ToLower()) ||
                x.Company.ToLower().Contains(query.SearchTerm.ToLower()));

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

            var gameCategory = db.Category
                .Select(x => new CategoryFormModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();


            if (products == null) return NotFound();

            var viewmodel = new AllGamesQueryModel();

            viewmodel.Categories = gameCategory;

            viewmodel.Products = products;

            viewmodel.CurrentPage = query.CurrentPage;

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
