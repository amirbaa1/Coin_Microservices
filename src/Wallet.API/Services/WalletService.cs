
using MongoDB.Driver;
using SharpCompress.Common;
using Wallet.API.Data;
using Wallet.API.Model;

namespace Wallet.API.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletdbContext _dbContext;

        public WalletService(IWalletdbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddWallet(WalletModel wallet)    
        {
            //var existingWalletCursor = await _dbContext.wallets.FindAsync(x => x.UserName == username);
            //var existingWallet = await existingWalletCursor.FirstOrDefaultAsync();

            await _dbContext.wallets.InsertOneAsync(wallet);

        }

        public async Task<IEnumerable<WalletModel>> GetUserNameWallet(string username)
        {
            FilterDefinition<WalletModel> filter = Builders<WalletModel>.Filter.Eq(x=>x.UserName, username);
            return await _dbContext.wallets.Find(filter).ToListAsync();
        }
    }
}
