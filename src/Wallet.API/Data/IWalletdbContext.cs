using MongoDB.Driver;
using Wallet.API.Model;

namespace Wallet.API.Data
{
    public interface IWalletdbContext
    {
        IMongoCollection<WalletModel> wallets { get; }
    }
}
