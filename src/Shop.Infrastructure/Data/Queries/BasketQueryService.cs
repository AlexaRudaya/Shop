using Microsoft.EntityFrameworkCore;
using Shop.ApplicationCore.Interfaces;

namespace Shop.Infrastructure.Data.Queries
{
    public sealed class BasketQueryService : IBasketQueryService
    {
        private readonly CatalogContext _dbContext;
        public BasketQueryService(CatalogContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task<int> CountTotalBasketItems(string userName)
        {
            var totalItems = await _dbContext.Baskets
                .Where(_=>_.BuyerId == userName)
                .SelectMany(_=>_.Items)
                .SumAsync(_=>_.Quantity);

            return totalItems; 
        }
    }
}
