using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BasketCheckOutEvent
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; } = "null";

        // Payment
        public string? CardName { get; set; } = "null";
        public string? CardNumber { get; set; } = "null";
        public string? Expiration { get; set; } = "null";
        public string? CVV { get; set; } = "null";
        public int? PaymentMethod { get; set; } = 0;
    }
}
