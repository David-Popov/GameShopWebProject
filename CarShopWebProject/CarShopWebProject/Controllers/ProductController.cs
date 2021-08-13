using CarShopWebProject.Data;
using CarShopWebProject.Models;
using CarShopWebProject.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Add()
        {
            return View(new ProductFormModel { Categories = productService.GetProductCategories(), Platforms = productService.GetProductPlatforms() });
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult Add(ProductFormModel product)
        {
            if (!this.db.Category.Any(c => c.Id.ToString() == product.CategoryId))
            {
                ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist");
            }

            if (!this.db.Platform.Any(c => c.Id.ToString() == product.PlatformId))
            {
                ModelState.AddModelError(nameof(product.PlatformId), "Platform does not exist");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = productService.GetProductCategories();
                product.Platforms = productService.GetProductPlatforms();

                return View(product);
            }
            else
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
        }

        public IActionResult Platforms(string id, [FromQuery] AllGamesQueryModel query)
        {
            var productQuerry = db.Product.AsQueryable();

            if (id == null)
            {
                return BadRequest();
            }

            var products = productService.GetProductsByPlatformId(id, query);

            if (!string.IsNullOrEmpty(query.SelectedCategory))
            {
                products = productService.SelectByCategory(id, query);
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

            var gameCategory = productService.GetCategories();

            if (products == null) return NotFound();

            var viewmodel = new AllGamesQueryModel();

            viewmodel.Categories = gameCategory;

            viewmodel.Products = products;

            viewmodel.CurrentPage = query.CurrentPage;

            return View(viewmodel);
        }

    }
}
