using MediatR;

namespace Wallet.Application.Features.Wallet.GetWalletList;

public class GetWalletListQuery : IRequest<List<WalletCoinVm>>
{
    public string UserName { get; set; }

    public GetWalletListQuery(string userName)
    {
        UserName = userName;
    }
}