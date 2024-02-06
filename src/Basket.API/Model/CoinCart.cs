namespace Basket.API.Model
{
    public class CoinCart
    {
        public string UserName { get; set; }
        public CoinCartList CoinCarts { get; set; } =new CoinCartList();
        public CoinCart(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                total = CoinCarts.PriceCoin * CoinCarts.Amount;
                return total;
            }
        }
    }
}
