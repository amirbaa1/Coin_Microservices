using MongoDB.Driver;
using Wallet.Application.Contracts;
using Wallet.Infrastructure.Data;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Repositories;

internal class WalletRepository : RepositoryBase<Wallet>, IWalletRepository
{
    public WalletRepository(WalletDbContext walletDbContext) : base(walletDbContext)
    {
    }
    
    public async Task<IEnumerable<Domain.Entities.Wallet>> GetWalletByUsername(string username)
    {
        var filter = Builders<Domain.Entities.Wallet>.Filter.Eq(w => w.UserName, username);
        var wallets = await _walletDbContext.wallets.Find(filter).ToListAsync();
        return wallets;
        
    }
}