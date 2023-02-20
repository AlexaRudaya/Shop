using Shop.ApplicationCore.Entites;

namespace Shop.ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        public Task<Basket> AddItem2Basket(string userName, int catalogItemId, decimal price, int quantity = 1);
    }
}
