using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.API.Handlers.UpdateWallet
{
    public class WalletCommand : IRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public List<WalletCoinCommand> walletCoins { get; set; }

        public void AddCoin(WalletCoinCommand coin)
        {
            var existingCoin = walletCoins.FirstOrDefault(x => x.NameCoin == coin.NameCoin);
            if (existingCoin != null)
            {
                existingCoin.Amount += coin.Amount;
                existingCoin.PriceUSD += coin.PriceUSD;
            }
            else
            {
                walletCoins.Add(coin);
            }
        }
    }
}
