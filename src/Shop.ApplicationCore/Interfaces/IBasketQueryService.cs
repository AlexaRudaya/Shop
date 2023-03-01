namespace Shop.ApplicationCore.Interfaces
{
    public interface IBasketQueryService
    {
        public Task<int> CountTotalBasketItems(string userName);
    }
}
