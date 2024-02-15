namespace WebApp.Model.OrderModel;

public class OrderModel
{
    public string UserName { get; set; }
    public string CoinName { get; set; }
    public int CoinId { get; set; }
    public double PriceCoin { get; set; }
    public double Amount { get; set; }
    public double TotalPrice { get; set; }
    public DateTime? DateTime { get; set; }

    // BillingAddress
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string AddressLine { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    // Payment
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public int PaymentMethod { get; set; }
}