namespace WebApp.Model.Basket
{
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketCoinModel> Items { get; set; } = new List<BasketCoinModel>();
        public double TotalPrice { get; set; }
    }
}
