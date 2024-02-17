using Identity.API.Data;
using Identity.API.Model;
using Identity.API.Model.Mail;
using Identity.API.Services.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IdentityAppdbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthService> _logger;
        private readonly IEmailService _emailService;

        public AuthService(IdentityAppdbContext context, UserManager<AppUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator, ILogger<AuthService> logger, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<LoginResponseDto> Login(LoginModel login)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.UserName.ToLower() == login.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, login.Password);
            if (isValid == false)
            {
                return new LoginResponseDto()
                {
                    UserDto = null,
                    Token = ""
                };
            }

            var CreateToken = _jwtTokenGenerator.GeneratorToken(user);
            UserDto userDto = new UserDto
            {
                ID = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return new LoginResponseDto()
            {
                UserDto = userDto,
                Token = CreateToken,
            };
        }

        public async Task<ChangePasswordModel> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordModel.UserName);
            if (user == null)
            {
                return null;
            }

            // Ensure that the current password is correct before changing it
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, changePasswordModel.Password);
            if (!isCurrentPasswordValid)
            {
                // Handle the case where the current password is not valid
                return null;
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.Password,
                changePasswordModel.NewPassword);

            if (result.Succeeded)
            {
                return new ChangePasswordModel
                {
                    UserName = changePasswordModel.UserName,
                    Password = changePasswordModel.NewPassword, // Updated to new password
                    NewPassword = null, // Reset to null as it's no longer applicable
                };
            }

            return null;
        }

        public async Task<string> Register(RegisterModel register)
        {
            AppUser appUser = new AppUser
            {
                Name = register.Name,
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                NormalizedEmail = register.Email.ToUpper(),
            };
            try
            {
                var result = await _userManager.CreateAsync(appUser, register.Password);
                //_logger.LogInformation($"result : {result}");
                if (result.Succeeded)
                {
                    var UserToReturn = _context.users.FirstOrDefault(x => x.UserName == register.Email);
                    //_logger.LogInformation($"UserToReturn : {UserToReturn}");
                    UserDto user = new UserDto
                    {
                        ID = UserToReturn.Id,
                        Name = UserToReturn.Name,
                        Email = UserToReturn.Email,
                        PhoneNumber = UserToReturn.PhoneNumber
                    };
                    Email email = new Email
                    {
                        To = UserToReturn.Email,
                        Body = $"TOken",
                        From = "amir.2002.ba@gmail.com",
                        Subject = "Token API Active."
                    };
                    await _emailService.SendEmail(email);
                    return "Create Register";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in code :{ex.Message}");
            }

            return "error";
        }
    }
}