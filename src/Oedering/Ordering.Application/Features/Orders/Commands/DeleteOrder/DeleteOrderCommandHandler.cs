using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Excptions;
using Ordering.Domain.Entities;


namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private ILogger<DeleteOrderCommand> _logger;
        public DeleteOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, ILogger<DeleteOrderCommand> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderUser = await _orderRepository.GetByIDAsync(request.Id);
            if (orderUser == null)
            {
                _logger.LogInformation("NOtFound UserName in Database");
                throw new NotFoundExcption(nameof(Order), request.Id);
            }

            await _orderRepository.DeleteAsAsync(orderUser);
            _logger.LogInformation($"{request.Id} deleted");
        }
    }
}
