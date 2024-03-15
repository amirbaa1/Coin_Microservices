using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Wallet.API.Model;

namespace Wallet.API.Data
{
    //public class WalletDbContext : DbContext
    //{
    //    public DbSet<WalletModel> walletModels { get; set; }
    //    public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
    //    {
    //    }
    //}
    public class WalletDbContext : IWalletdbContext
    {
        private readonly IConfiguration _configuration;

        public WalletDbContext(IConfiguration configuration)
        {
            var clinet = new MongoClient(configuration.GetValue<string>("WalletStoreDatabase:ConnectionString"));
            var database = clinet.GetDatabase(configuration.GetValue<string>("WalletStoreDatabase:DatabaseName"));

            wallets = database.GetCollection<WalletModel>(configuration.GetValue<string>("WalletStoreDatabase:WalletsCollectionName"));
        }
        public IMongoCollection<WalletModel> wallets { get; set; }

    }
}
