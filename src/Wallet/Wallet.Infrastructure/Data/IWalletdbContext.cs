using MongoDB.Driver;

namespace Wallet.Infrastructure.Data
{
    public interface IWalletdbContext
    {
        IMongoCollection<Domain.Entities.Wallet> wallets { get; }
    }
}
