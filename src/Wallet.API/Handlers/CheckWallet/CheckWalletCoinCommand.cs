using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Wallet.API.Handlers.CheckWallet
{
    public class CheckWalletCoinCommand
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string NameCoin { get; set; }
        public double Amount { get; set; }
        public double coinPrice { get; set; }
        public double PriceUSD { get; set; }
    }
}
