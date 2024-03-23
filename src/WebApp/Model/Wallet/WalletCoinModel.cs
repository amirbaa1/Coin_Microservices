
namespace WebApp.Model
{
    public class WalletCoinModel
    {
        // [BsonId]
        // [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        // public ObjectId Id { get; set; }
        public string NameCoin { get; set; }
        public string Symbol { get; set; }
        public double Amount { get; set; }
        public double coinPrice { get; set; }
        public double PriceUSD { get; set; }
    }
}
