namespace Shop.ApplicationCore.Entites
{
    public sealed class Basket
    {
        public int Id { get; set; }
        public string? BuyerId { get; set; }

        public Basket(string userName)
        {
            BuyerId = userName;
        }
        public Basket()
        {

        }
    }
}
