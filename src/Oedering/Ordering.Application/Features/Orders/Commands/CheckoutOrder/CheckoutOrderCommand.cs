﻿using MediatR;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public string CoinName { get; set; }
        public int CoinId { get; set; }
        public double PriceCoin { get; set; }
        public double Amount { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime? DateTime { get; set; }


        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }


        // Payment
        public string? CardName { get; set; } = "null";
        public string? CardNumber { get; set; } = "null";
        public string? Expiration { get; set; } = "null";
    }
}