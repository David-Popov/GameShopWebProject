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
    public class GiftCardController : Controller
    {
        private readonly GameShopDbContext db;
        private readonly IProductService productService;

        public GiftCardController(GameShopDbContext db, IProductService productService)
        {
            this.db = db;
            this.productService = productService;
        }

        public IActionResult AddGiftCard()
        {
            return View(new GiftCardFormModel {Platforms = productService.GetProductPlatforms() });
        }

        [HttpPost]
        public IActionResult AddGiftCard(GiftCardFormModel giftcard)
        {
            foreach (var products in db.GiftCards)
            {
                if (products.Tittle == giftcard.Tittle)
                {
                    ModelState.AddModelError(nameof(giftcard.Tittle), "This gift card already exists");
                }
            }

            if (!this.db.Platform.Any(c => c.Id.ToString() == giftcard.PlatformId))
            {
                ModelState.AddModelError(nameof(giftcard.PlatformId), "Platform does not exist");
            }

            if (!ModelState.IsValid)
            {
                giftcard.Platforms = productService.GetProductPlatforms();

                return View(giftcard);
            }
            else
            {
                productService.AddGiftCard(
                giftcard.Tittle,
                giftcard.Price,
                giftcard.Description,
                giftcard.ImageUrl,
                giftcard.PlatformId);

                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult GiftCards(string id, [FromQuery] AllGamesQueryModel query)
        {
            var giftCardQuerry = db.GiftCards.AsQueryable();

            if (id == null)
            {
                return BadRequest();
            }

            var giftCards = productService.GetGiftCardByPlatformId(id, query);


            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                giftCardQuerry = giftCardQuerry.Where(x =>
                x.Tittle.ToLower().Contains(query.SearchTerm.ToLower()));

                giftCards = giftCardQuerry
               .OrderByDescending(c => c.Id)
               .Select(x => new GiftCardFormModel
               {
                   Tittle = x.Tittle,
                   Price = x.Price,
                   Description = x.Description,
                   ImageUrl = x.ImageUrl,
                   PlatformId = x.PlatformId
               }).ToList();

            }

            var gameCategory = productService.GetCategories();

            if (giftCards == null) return NotFound();

            var viewmodel = new AllGamesQueryModel();

            viewmodel.GiftCards = giftCards;

            viewmodel.CurrentPage = query.CurrentPage;

            return View(viewmodel);
        }


    }
}
