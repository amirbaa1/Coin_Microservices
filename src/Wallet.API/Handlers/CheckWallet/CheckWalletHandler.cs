using AutoMapper;
using MediatR;
using MongoDB.Bson;
using Wallet.API.Model;
using Wallet.API.Services;

namespace Wallet.API.Handlers.CheckWallet
{
    public class CheckWalletHandler : IRequestHandler<CheckWalletCommand, ObjectId>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckWalletHandler> _logger;

        public CheckWalletHandler(IWalletService walletService, IMapper mapper, ILogger<CheckWalletHandler> logger)
        {
            _walletService = walletService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ObjectId> Handle(CheckWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = _mapper.Map<WalletModel>(request);
            await _walletService.AddWallet(wallet);

            _logger.LogInformation($"Wallet Id {wallet.Id} is successfully created.");

            return wallet.Id;
        }
    }
}
