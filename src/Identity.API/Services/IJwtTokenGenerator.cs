using Identity.API.Model;

namespace Identity.API.Services
{
    public interface IJwtTokenGenerator
    {
        string GeneratorToken(AppUser appUser);
    }
}
