using Identity.API.Model;

namespace Identity.API.Services
{
    public interface IAuthService
    {
        //Task<List<UserDto>> GetAll();
        Task<string> Register(RegisterModel register);
        Task<LoginResponseDto> Login(LoginModel login);
        //Task<bool> AssignRole(string email, string roleName);
    }
}
