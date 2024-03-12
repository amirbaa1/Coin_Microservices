namespace WebApp.Model.Basket
{
    public class CoinCart
    {
        public string UserName { get; set; }
        public CoinCartList CoinCarts { get; set; } = new CoinCartList();

        public double TotalPrice
        {
            get
            {
                double total = 0;
                total = CoinCarts.PriceCoin * CoinCarts.Amount;
                return total;
            }
        }
        public CoinCart() 
        {
        }

        public CoinCart(string username)
        {
            UserName = username;
        }
    }
}
