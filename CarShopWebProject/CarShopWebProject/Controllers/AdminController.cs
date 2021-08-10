using CarShopWebProject.Data;
using CarShopWebProject.Services;
using Microsoft.AspNetCore.Mvc;


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
    }
}
