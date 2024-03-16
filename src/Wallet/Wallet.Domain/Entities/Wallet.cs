using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Wallet.Domain.Common;

namespace Wallet.Domain.Entities;

public class Wallet : EntityBase
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public string UserName { get; set; }
    public List<WalletCoin> walletCoins { get; set; }

    public void AddCoin(WalletCoin coin)
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