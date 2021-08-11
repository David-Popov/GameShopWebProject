using CarShopWebProject.Data;
using CarShopWebProject.Models;
using CarShopWebProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarShopWebProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly GameShopDbContext db;
        private readonly IProductService productService;
        public AdminController(GameShopDbContext db, IProductService productService)
        {
            this.db = db;
            this.productService = productService;
        }
        public IActionResult CreateAdmin()
        {
            return View();
        }

        public IActionResult ManageProducts()
        {
            var prodcutList = db.Product;
               
            return View(prodcutList);
        }

        public IActionResult DeleteProduct(string id)
        {
            var productToDelete = productService.GetDbProduct(id).FirstOrDefault();
               

            return View(productToDelete);
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

            return RedirectToAction("ManageProducts", "Admin");
        }

        
        public IActionResult BackToList()
        {
            return RedirectToAction("ManageProducts", "Admin");
        }
    }
}
