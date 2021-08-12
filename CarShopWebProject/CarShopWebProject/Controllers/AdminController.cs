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
           var products = productService.GetProduct();
           
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

            var products = productService.GetProduct();

            return RedirectToAction("ManageProducts", "Admin");
        }

        public IActionResult EditProduct(string id)
        {
            var productForEdit = productService.GetDbProduct(id).FirstOrDefault();

            return View(productForEdit);
        }

        [HttpPost]
        public IActionResult EditProduct(EditFormModel product)
        {
            var productForEdit = productService.GetDbProduct(product.Id).FirstOrDefault();

            productForEdit.Id = int.Parse(product.Id);
            productForEdit.Tittle = product.Tittle;
            productForEdit.Price = product.Price;
            productForEdit.Year = product.Year;
            productForEdit.Company = product.Company;
            productForEdit.ImageUrl = product.ImageUrl;
            productForEdit.Description = product.Description;

            db.Attach(productForEdit);
            db.Entry(productForEdit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("ManageProducts", "Admin");
        }

        public IActionResult BackToList()
        {
            return RedirectToAction("ManageProducts", "Admin");
        }
    }
}
