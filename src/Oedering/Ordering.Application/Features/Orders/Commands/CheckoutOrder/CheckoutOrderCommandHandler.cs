using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Infrasturcture;
using Ordering.Application.Contracts.Persistence;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Ordering.Domain.Entities;
using Ordering.Application.Model;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var OrderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(OrderEntity);

            _logger.LogInformation($"order Id {newOrder.Id} is successfully create.");
            await SendMail(newOrder);
            return newOrder.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new Email()
            {
                To = order.EmailAddress,
                From = "amir.2002.ba@gmail.com",
                Body = "Order was Create",
                Subject = "Order Create",
            };
            try
            {
                bool emailSent = await _emailService.sendEmail(email);
                if (emailSent)
                {
                    await _emailService.sendEmail(email);
                    _logger.LogInformation($"Email sent successfully for order {order.EmailAddress}.");
                }
                else
                {
                    _logger.LogError($"Failed Email sent for order {order.EmailAddress}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"order {order.Id} error with the mail service :{ex.Message}");
            }
        }
    }
}