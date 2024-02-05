using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Excptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdadeOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdadeOrderCommand> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdadeOrderCommand> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdadeOrderCommand request, CancellationToken cancellationToken)
        {
          var order = await  _orderRepository.GetByIDAsync(request.Id);
           if(order == null)
            {
                _logger.LogInformation("NotFound in Database");
                throw new NotFoundExcption(nameof(Order), request.Id);
            }
            _mapper.Map(request, order, typeof(UpdadeOrderCommand), typeof(Order));
            await _orderRepository.UpdateAsycn(order);
            _logger.LogInformation($"Order {order.Id} is succffully Update");
            //return Unit.Value;
        }
    }
}
