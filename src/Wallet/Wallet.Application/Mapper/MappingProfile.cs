using AutoMapper;
using Wallet.Application.Features.Wallet.Common.CheckWallet;
using Wallet.Application.Features.Wallet.GetWalletList;

namespace Wallet.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Wallet, WalletVm>().ReverseMap();
        CreateMap<Domain.Entities.Wallet, CheckWalletCommon>().ReverseMap();
    }
}