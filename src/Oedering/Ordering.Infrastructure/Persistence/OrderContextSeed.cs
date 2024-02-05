﻿using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        private static bool _isSeeded = false;
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.orders.Any())
            {
                context.orders.AddRange(GetUserOrder());
                await context.SaveChangesAsync();   
                logger.LogInformation("seed save in database with context {DbContextName}",typeof(OrderContextSeed).Name);
                _isSeeded = true;
            }
        }

        private static IEnumerable<Order> GetUserOrder()
        {
            return new List<Order>
            {
                new Order
                {
                    UserName= "swm",
                    FirstName = "sw",
                    LastName="m",
                    EmailAddress="s@m.com",
                    AddressLine = "NYC",
                    Country="USA",
                    TotalPrice = 360,
                },
                new Order
                {

                    UserName = "amiba",
                    FirstName = "amir",
                    LastName = "ba",
                    EmailAddress="amir.2002.ba@gmail.com",
                    AddressLine="Jant",
                    Country = "Iran",
                    TotalPrice = 200,
                }
            };
        }
    }
}
