using Microsoft.AspNetCore.Identity;

namespace WebApp.Model.AccountModel
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}