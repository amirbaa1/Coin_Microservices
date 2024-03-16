using Microsoft.AspNetCore.Mvc;
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
            await _dbContext.wallets.InsertOneAsync(wallet);
        }

        public async Task UpdateWallet(WalletModel wallet)
        {
            // var chenge = wallet.walletCoins(wallet.walletCoins.First());
            var existingWallet = await _dbContext.wallets.Find(g => g.Id == wallet.Id).FirstOrDefaultAsync();

            existingWallet.AddCoin(wallet.walletCoins.First());

            await _dbContext.wallets.ReplaceOneAsync(g => g.Id == wallet.Id, existingWallet);
        }

        public async Task<IEnumerable<WalletModel>> GetUserNameWallet(string username)
        {
            FilterDefinition<WalletModel> filter = Builders<WalletModel>.Filter.Eq(x => x.UserName, username);
            return await _dbContext.wallets.Find(filter).ToListAsync();
        }
    }
}