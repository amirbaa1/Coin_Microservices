using Identity.API.Model;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Services
{
    public interface IAuthService
    {
        //Task<List<UserDto>> GetAll();
        Task<string> Register(RegisterModel register);

        Task<LoginResponseDto> Login(LoginModel login);

        Task<bool> AssignRole(string email, string roleName);
        Task<ChangePasswordModel> ChangePassword(ChangePasswordModel changePasswordModel);
        Task<bool> SendPasswordResetToken(string email);
        Task<IdentityResult> RestPassword(RestPassword restPassword);
    }
}