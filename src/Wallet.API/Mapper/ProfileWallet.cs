using AutoMapper;
using EventBus.Messages.Events;
using Wallet.API.Model;

namespace Wallet.API.Mapping
{
    public class ProfileWallet : Profile
    {
        public ProfileWallet()
        {
            CreateMap<WalletModel,BasketWalletEvent>().ReverseMap();
        }
    }
}
