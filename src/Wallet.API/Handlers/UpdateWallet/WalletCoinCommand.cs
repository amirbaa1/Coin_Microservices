using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.API.Handlers.UpdateWallet
{
    public class WalletCoinCommand : IRequest<int>
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
