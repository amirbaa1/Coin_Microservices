// using Identity.API.Model;
// using Microsoft.AspNetCore.Identity;
//
// namespace Identity.API.Data;
//
// public class IdentitydbSeed
// {
//     private static bool _isSeeded = false;
//
//     public static async Task SeedAsync(IdentityAppdbContext context, ILogger<IdentitydbSeed> logger)
//     {
//         logger.LogInformation("Seeding data...");
//         logger.LogInformation($"-----> GET User :  {GetUserOrder()} <-----");
//         logger.LogInformation($"{context.users.Any()}");
//         if (!context.users.Any())
//         {
//             logger.LogInformation($"-----> GET User :  {GetUserOrder()} <-----");
//             context.users.AddRange(GetUserOrder());
//             await context.SaveChangesAsync();
//             logger.LogInformation("seed save in database with context {DbContextName}", typeof(IdentitydbSeed).Name);
//             _isSeeded = true;
//         }
//     }
//
//     private static IEnumerable<RegisterModel> GetUserOrder()
//     {
//         return new List<RegisterModel>
//         {
//             new RegisterModel
//             {
//                 Email = "a@b.com",
//                 Name = "Admin",
//                 PhoneNumber = "098754e3", 
//                 Password = "Admin@123",
//                 Role = "adminRole"
//             },
//         };
//     }
// }