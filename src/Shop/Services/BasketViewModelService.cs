using Shop.ApplicationCore.Entites;
using Shop.ApplicationCore.Interfaces;
using Shop.ApplicationCore.Services;
using Shop.Interfaces;
using Shop.Models;
using Shop.Pages.Basket;

namespace Shop.Services
{
    public sealed class BasketViewModelService : IBasketViewModelService
    {
        private readonly IBasketQueryService _basketQueryService;
        private readonly IRepository<CatalogItem> _itemRepository;
        private readonly IUriComposer _uriComposer;
        public BasketViewModelService(IBasketQueryService basketQueryService, 
            IRepository<CatalogItem> itemRepository, IUriComposer uriComposer)
        {
            _basketQueryService = basketQueryService; 
            _itemRepository = itemRepository;
            _uriComposer = uriComposer;
        }
        public async Task<int> CountTotalBasketItems(string userName)
        {
            var amount = await _basketQueryService.CountTotalBasketItems(userName);
            return amount;
        }

        public Task<BasketViewModel> GetOrCreateBasketForUser(string userName)
        {
            return null;
        }

        public async Task<BasketViewModel> Map(Basket basket)
        {
            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = await GetBasketItems(basket.Items)
            };
        }

        private async Task<List<BasketItemViewModel>> GetBasketItems(IReadOnlyCollection<BasketItem> basketItems)
        {
            var catalogItems = await _itemRepository.GetAllAsync();

            var items = basketItems.Select(basketItem =>
            {
                var catalogItem = catalogItems.First(c => c.Id == basketItem.CatalogItemId);

                var basketItemViewModel = new BasketItemViewModel
                {
                    Id = basketItem.Id,
                    UnitPrice = basketItem.UnitPrice,
                    Quantity = basketItem.Quantity,
                    CatalogItemId = basketItem.CatalogItemId,
                    PictureUrl = _uriComposer.ComposeImageUri(catalogItem.PictureUrl),
                    ProductName = catalogItem.Name
                };
                return basketItemViewModel;
            }).ToList();

            return items;
        }
    }
}
