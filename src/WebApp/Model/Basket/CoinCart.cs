namespace WebApp.Model.Basket
{
    public class CoinCart
    {
        public string UserName { get; set; }
        public List<CoinCartList> CoinCarts { get; set; } = new List<CoinCartList>();

        public double TotalPrice { get; set; }
        //     public double TotalPrice
        //     {
        //         get
        //         {
        //             double total = 0;
        //             total = CoinCarts.PriceCoin * CoinCarts.Amount;
        //             return total;
        //         }
        //     }
        //
        public CoinCart(string username)
        {
            UserName = username;
        }
        // }
    }
}