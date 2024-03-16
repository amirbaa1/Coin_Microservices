using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Wallet.Application.Contracts;

namespace Wallet.Application.Features.Wallet.Common.CheckWallet;

public class CheckWalletHandler : IRequestHandler<CheckWalletCommon, ObjectId>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CheckWalletHandler> _logger;
    private readonly IWalletRepository _repository;

    public CheckWalletHandler(IMapper mapper, ILogger<CheckWalletHandler> logger, IWalletRepository repository)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    public async Task<ObjectId> Handle(CheckWalletCommon request, CancellationToken cancellationToken)
    {
        var walletEnt = _mapper.Map<Domain.Entities.Wallet>(request);
        var newWallet = await _repository.AddAsync(walletEnt);

        _logger.LogInformation($"order Id {newWallet.Id} is successfully create.");

        return newWallet.Id;
    }
}