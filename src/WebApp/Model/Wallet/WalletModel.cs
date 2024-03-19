using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApp.Model
{
    public class WalletModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public List<WalletCoinModel> walletCoins { get; set; }

        public void AddCoin(WalletCoinModel coin)
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
