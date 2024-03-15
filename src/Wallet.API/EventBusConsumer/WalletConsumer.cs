using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Wallet.API.Handlers.CheckWallet;
using Wallet.API.Handlers.UpdateWallet;

namespace Wallet.API.EventBusConsumer
{
    public class WalletConsumer : IConsumer<BasketWalletEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<WalletConsumer> _logger;

        public WalletConsumer(IMapper mapper, IMediator mediator, ILogger<WalletConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BasketWalletEvent> context)
        {
            var command = _mapper.Map<CheckWalletCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("++++++> WalletEvent consumed successfully. Created Wallet Id : {newOrderId}", result);

        }
    }
}
