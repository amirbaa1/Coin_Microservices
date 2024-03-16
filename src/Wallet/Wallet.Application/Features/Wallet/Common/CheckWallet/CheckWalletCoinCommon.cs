using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.Application.Features.Wallet.Common.CheckWallet;

public class CheckWalletCoinCommon : IRequest<ObjectId>
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public string NameCoin { get; set; }
    public double Amount { get; set; }
    public double coinPrice { get; set; }
    public double PriceUSD { get; set; }
}