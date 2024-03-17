using Newtonsoft.Json;
using MongoDB.Driver;
using Wallet.API.Data;
using Wallet.API.Model;

namespace Wallet.API.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletdbContext _dbContext;
        private readonly ILogger<WalletService> _logger;
        public WalletService(IWalletdbContext dbContext, ILogger<WalletService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddWallet(WalletModel wallet)
        {
            await _dbContext.wallets.InsertOneAsync(wallet);
        }

        public async Task UpdateWallet(WalletModel wallet)
        {
            var userWallet = await _dbContext.wallets.Find(x => x.UserName == wallet.UserName).FirstOrDefaultAsync();
            //var existingWallet = await _dbContext.wallets.Find(g => g.Id == wallet.Id).FirstOrDefaultAsync();

            if (userWallet != null)
            {

                userWallet.AddCoin(wallet.walletCoins.First());
                _logger.LogInformation($"---- >  {JsonConvert.SerializeObject(userWallet)}");
                await _dbContext.wallets.ReplaceOneAsync(g => g.UserName == wallet.UserName, userWallet);

            }
            else
            {
                throw new Exception("Wallet not found with the specified Id.");
            }
        }

        public async Task<IEnumerable<WalletModel>> GetUserNameWallet(string username)
        {
            FilterDefinition<WalletModel> filter = Builders<WalletModel>.Filter.Eq(x => x.UserName, username);
            return await _dbContext.wallets.Find(filter).ToListAsync();
        }
    }
}