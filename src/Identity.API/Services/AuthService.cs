using Identity.API.Data;
using Identity.API.Model;
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
        public AuthService(IdentityAppdbContext context, UserManager<AppUser> userManager, IJwtTokenGenerator jwtTokenGenerator, ILogger<AuthService> logger)
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
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
