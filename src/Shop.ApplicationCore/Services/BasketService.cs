using Shop.ApplicationCore.Entites;
using Shop.ApplicationCore.Interfaces;

namespace Shop.ApplicationCore.Services
{
    public sealed class Basketservice : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;

        public Basketservice(IRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> AddItem2Basket(string userName, int catalogItemId, decimal price, int quantity = 1)
        {
            var basket = await _basketRepository.FirstOrDefaultAsync(_=>_.BuyerId == userName);

            if (basket is null)
            {
                basket= new Basket(userName);
                basket = await _basketRepository.AddAsync(basket);
            }
            
            basket.AddItem(catalogItemId, price, quantity);
            await _basketRepository.UpdateAsync(basket);

            return basket;
        }
    }
}
