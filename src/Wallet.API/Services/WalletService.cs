using Newtonsoft.Json;
using MongoDB.Driver;
using Wallet.API.Data;
using Wallet.API.Model;
using Wallet.API.Services.Coin;

namespace Wallet.API.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletdbContext _dbContext;
        private readonly ILogger<WalletService> _logger;
        private readonly ICoinService _coinService;
        public WalletService(IWalletdbContext dbContext, ILogger<WalletService> logger, ICoinService coinService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _coinService = coinService;
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

        public async Task<bool> UpdateCoinWallet(string username)
        {
            var GetWallet = await GetUserNameWallet(username);
            _logger.LogInformation($"Get wallet service : {GetWallet}");
            var round = 0;

            if (GetWallet == null)
            {
                throw new Exception("you have not wallet");
            }

            foreach (var i in GetWallet)
            {
                _logger.LogInformation($"All count : {i.walletCoins.Count()}");
                round = 0;

                foreach (var coin in i.walletCoins)
                {
                    var coinname = coin.NameCoin;
                    if (coinname != null)
                    {
                        var getCoin = await _coinService.GetCoinBySymbol(coinname);

                        foreach (var coinItem in getCoin.Data.Values)
                        {

                            foreach (var coinSearch in coinItem)
                            {
                                if (coinSearch.Quote != null && coinSearch.Quote.ContainsKey("USD"))
                                {
                                    coin.coinPrice = coinSearch.Quote["USD"].Price;
                                    coin.PriceUSD = coinSearch.Quote["USD"].Price * coin.Amount;

                                    _logger.LogInformation($"Update Coin service : {JsonConvert.SerializeObject(coin)}");
                                    _logger.LogInformation($"Update wallet service : {JsonConvert.SerializeObject(i)}");

                                    //var result = await _dbContext.wallets.UpdateOneAsync(
                                    //    g => g.UserName == i.UserName,
                                    //    Builders<WalletModel>.Update.Set(w => w.walletCoins, i.walletCoins));

                                    await _dbContext.wallets.ReplaceOneAsync(g => g.UserName == i.UserName, i);
                                    round++;
                                    _logger.LogInformation($"count1 : {round}");
                                    if (round == i.walletCoins.Count())
                                    {
                                        return true;
                                    }

                                    break;

                                }

                            }
                        }
                    }
                }


            }
            return false;
        }
    }
}