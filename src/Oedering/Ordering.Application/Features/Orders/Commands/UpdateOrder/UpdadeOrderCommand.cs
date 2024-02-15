using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdadeOrderCommand : IRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CoinName { get; set; }
        public int CoinId { get; set; }
        public double PriceCoin { get; set; }
        public double Amount { get; set; }
        public double TotalPrice { get; set; }

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