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

        public async Task<Basket> AddItem2Basket(string userName)
        {
            //TODO check if basket is alredy exist for this user
            Basket basket = default;

            if (basket is null)
            {
                basket= new Basket(userName);
                basket = await _basketRepository.AddAsync(basket);
            }
            
            return basket;
        }
    }
}
