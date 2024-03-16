using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Basket.API.Model
{
    public class WalletCoinModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string NameCoin { get; set; }
        public double Amount { get; set; }
        public double coinPrice { get; set; }
        public double PriceUSD { get; set; }
    }
}
