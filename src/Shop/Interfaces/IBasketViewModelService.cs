using Shop.ApplicationCore.Entites;
using Shop.Pages.Basket;

namespace Shop.Interfaces
{
    public interface IBasketViewModelService
    {
        public Task<int> CountTotalBasketItems(string userName);

        public Task<BasketViewModel> GetOrCreateBasketForUser(string userName);

        public Task<BasketViewModel> Map(Basket basket);
    }
}
