using Microsoft.AspNetCore.Mvc;
using Wallet.API.Model;

namespace Wallet.API.Services
{
    public interface IWalletService
    {
        Task<IEnumerable<WalletModel>> GetUserNameWallet(string username);

        //Task AddWallet(string username);
        Task AddWallet(WalletModel wallet);
        Task UpdateWallet(WalletModel wallet);
    }
}