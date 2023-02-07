﻿using Microsoft.AspNetCore.Mvc;
using Shop.ApplicationCore.Interfaces;
using Shop.Interfaces;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly IRepository<CatalogItem> _catalogRepository;

        public CatalogController(
            IRepository<CatalogItem> catalogRepository,
            ICatalogItemViewModelService catalogItemViewModelService)
        {
            _catalogItemViewModelService = catalogItemViewModelService;

            _catalogRepository = catalogRepository;
        }
        public async Task<IActionResult> Index(CatalogIndexViewModel model)
        {
            var viewModel = await _catalogItemViewModelService.GetCatalogItems(model.BrandFilterApplied, model.TypesFilterApplied);

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var item = _catalogRepository.GetById(id);
            if (item == null) return RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price
            };
            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _catalogRepository.GetById(id);
            if (item == null) return RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price
            };
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CatalogItemViewModel catalogItemViewModel)
        {
            try
            {
                _catalogItemViewModelService.UpdateCatalogItem(catalogItemViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        private string GetOrSetBasketCookieAndUserName()
        {
            string? userName = default;

            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                return this.Request.HttpContext.User.Identity.Name;
            }

            if (this.Request.Cookies.ContainsKey("eShop"))
            {
                userName = Request.Cookies["eShop"];

                if (!Request.HttpContext.User.Identity.IsAuthenticated)
                {
                    userName = default;
                }
            }

            if (userName != null) return userName;

            userName = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(30);

            Response.Cookies.Append("eShop", userName, cookieOptions);

            return userName;
        }
    }
}
