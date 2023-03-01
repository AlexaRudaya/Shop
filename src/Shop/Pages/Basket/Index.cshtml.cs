using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.ApplicationCore.Interfaces;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Pages.Basket
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<CatalogItem> _itemRepository;
        private readonly IBasketService _basketService;
        private readonly IBasketViewModelService _basketViewModelService;

        public IndexModel(IRepository<CatalogItem> itemRepository,
            IBasketService basketService, IBasketViewModelService basketViewModelService)
        {
            _itemRepository = itemRepository;
            _basketService = basketService;
            _basketViewModelService = basketViewModelService;
        }

        public BasketViewModel BasketModel { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id is null) 
            {
                return RedirectToPage("Index");
            }

            var item = await _itemRepository.GetByIdAsync(productDetails.Id);

            if (item == null) 
            {
                return RedirectToPage("Index");
            }

            var userName = GetOrSetBasketCookieAndUserName();

            var basket = await _basketService.AddItem2Basket(userName, productDetails.Id, item.Price);

            BasketModel = await _basketViewModelService.Map(basket);

            return RedirectToPage();
        }

        private string GetOrSetBasketCookieAndUserName()
        {
            string? userName = null;

            if (Request.HttpContext.User.Identity.IsAuthenticated)
            {
                return Request.HttpContext.User.Identity.Name!;
            }
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                userName = Request.Cookies[Constants.BASKET_COOKIENAME];

                if (!Request.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (!Guid.TryParse(userName, out var _))
                    {
                        userName = null;
                    }
                }
            }

            if (userName != null) return userName;

            userName = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddMonths(2);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, userName, cookieOptions);

            return userName;
        }
    }
}
