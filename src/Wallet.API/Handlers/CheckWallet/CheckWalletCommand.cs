using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MediatR;

namespace Wallet.API.Handlers.CheckWallet
{
    public class CheckWalletCommand : IRequest<ObjectId>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public List<CheckWalletCoinCommand> walletCoins { get; set; }

        public void AddCoin(CheckWalletCoinCommand coin)
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
