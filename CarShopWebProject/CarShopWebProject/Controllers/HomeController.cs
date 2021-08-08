﻿using CarShopWebProject.Data;
using CarShopWebProject.Models;
using CarShopWebProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameShopDbContext db;
        private readonly IProductService productService;
        public HomeController(GameShopDbContext db, IProductService productService)
        {
            this.db = db;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            ViewBag.Platforms = productService.GetProductPlatforms();

            var takeProduct = db
                .Product
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
                })
                .Take(3)
                .ToList();


            return View(takeProduct);
        }

        public IActionResult Privacy()
        {
            ViewBag.Platforms = productService.GetProductPlatforms();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<PlatformFormModel> GetProductPlatforms()
       => db.Platform
               .Select(x => new PlatformFormModel
               {
                   Id = x.Id,
                   Name = x.Name
               }).ToList();
    }
}
