using Microsoft.AspNetCore.Mvc;
using Shop.ApplicationCore.Interfaces;
using Shop.Interfaces;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly IBasketService _basketService;
        private readonly IRepository<CatalogItem> _catalogRepository;

        public CatalogController(
            IRepository<CatalogItem> catalogRepository,
            ICatalogItemViewModelService catalogItemViewModelService,
            IBasketService basketService)
        {
            _catalogItemViewModelService = catalogItemViewModelService;

            _basketService = basketService;

            _catalogRepository = catalogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied)
        {
            var userName = GetOrSetBasketCookieAndUserName();

            var viewModel = await _catalogItemViewModelService.GetCatalogItems(brandFilterApplied, typesFilterApplied);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id, decimal price)
        {
            var userName = GetOrSetBasketCookieAndUserName();
            var basket = await _basketService.AddItem2Basket(userName, id, price);

            return RedirectToAction("Index");
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
        public async Task<IActionResult> Edit(int id)
        {
            var items = await _catalogRepository.GetAllAsync();
            if (items == null) return RedirectToAction("Index");

            var item = items.First();
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
