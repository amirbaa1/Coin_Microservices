using AutoMapper;
using MediatR;
using Wallet.API.Model;
using Wallet.API.Services;

namespace Wallet.API.Handlers.UpdateWallet
{
    public class UpdateWalletHandler : IRequestHandler<WalletCommand>
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<UpdateWalletHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateWalletHandler(IWalletService walletService, ILogger<UpdateWalletHandler> logger, IMapper mapper)
        {
            _walletService = walletService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Handle(WalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _walletService.GetUserNameWallet(request.UserName);
            if (wallet == null)
            {
                _logger.LogInformation("Wallet not found in Database");
                return;
            }

            var walletModel = wallet.FirstOrDefault(); 
            if (walletModel == null)
            {
                _logger.LogInformation("Wallet not found in Database");
                return;
            }

            _mapper.Map(request, walletModel, typeof(WalletCommand), typeof(WalletModel));
            //await _walletService.AddWallet(request.UserName);
            _logger.LogInformation($"Wallet {walletModel.Id} is successfully updated");
        }
    }
}
