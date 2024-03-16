using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Wallet.Infrastructure.Data
{
    public class WalletDbContext : IWalletdbContext
    {
        private readonly IConfiguration _configuration;

        public WalletDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("WalletStoreDatabase"));
            var database = client.GetDatabase(configuration["WalletStoreDatabase:DatabaseName"]);

            wallets = database.GetCollection<Domain.Entities.Wallet>(
                configuration["WalletStoreDatabase:WalletsCollectionName"]);
        }

        public IMongoCollection<Domain.Entities.Wallet> wallets { get; set; }
    }
}