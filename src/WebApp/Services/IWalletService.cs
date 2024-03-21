using Microsoft.AspNetCore.Mvc;
using WebApp.Model;

namespace WebApp.Services
{
    public interface IWalletService
    {
        //Task<WalletModel> PostWallet(WalletModel wallet);
        Task<List<WalletModel>> OnGetWalletByUserName(string userName);

        //Task<List<WalletModel>> UpdateCoiWallet(string userame,WalletModel wallet);
    }
}
