using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.Application.Features.Wallet.GetWalletList;

public class WalletVm : IRequest<ObjectId>
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public string UserName { get; set; }
    public List<WalletCoinVm> walletCoins { get; set; }

    public void AddCoin(WalletCoinVm coin)
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