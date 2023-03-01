namespace Shop.Pages.Basket
{
    public sealed class BasketViewModel
    {
        public int Id { get; set; }

        public List<BasketItemViewModel> Items { get; set; } = new();

        public string? BuyerId { get; set; }

        public decimal Total()
        {
            return Math.Round(Items.Sum(_=>_.UnitPrice * _.Quantity),2);
        }
    }
}
