using Shop.ApplicationCore.Entites;

namespace Shop.ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        public Task<Basket> AddItem2Basket(string userName);
    }
}
