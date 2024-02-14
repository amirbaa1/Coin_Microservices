namespace Basket.API.Model
{
    public class CheckOut
    {
        public string UserName { get; set; }
        public string CoinName { get; set; }
        public int CoinId { get; set; }
        public decimal PriceCoin { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? DateTime { get; set; }

        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }


        // Payment
        public string? CardName { get; set; } = "null";
        public string? CardNumber { get; set; } = "null";
        public string? Expiration { get; set; } = "null";
    }
}
