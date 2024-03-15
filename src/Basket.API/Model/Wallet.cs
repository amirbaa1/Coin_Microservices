namespace Basket.API.Model
{
    public class Wallet
    {
        public string UserName { get; set; }
        public string CoinName { get; set; }
        public double PriceCoin { get; set; }
        public double Amount { get; set; }
        public double TotalPrice { get; set; }
        //public DateTime? DateTime { get; set; }
    }
}
