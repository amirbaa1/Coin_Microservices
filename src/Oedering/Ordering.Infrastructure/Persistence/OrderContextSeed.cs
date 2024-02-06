using Microsoft.Extensions.Logging;
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
            logger.LogInformation("Seeding data...");
            logger.LogInformation($"-----> GET User :  {GetUserOrder()} <-----");
            logger.LogInformation($"{context.orders.Any()}");
            if (!context.orders.Any())
            {
                logger.LogInformation($"-----> GET User :  {GetUserOrder()} <-----");
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
                    State = "DefaultState",
                    TotalPrice = 360,
                },
                new Order
                {

                    UserName = "amirba",
                    FirstName = "amir",
                    LastName = "ba",
                    EmailAddress="amir.2002.ba@gmail.com",
                    AddressLine="Jant",
                    Country = "Iran",
                    State = "DefaultState",
                    TotalPrice = 200,
                }
            };
        }
    }
}
