using Microsoft.AspNetCore.Identity;

namespace Identity.API.Model
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}