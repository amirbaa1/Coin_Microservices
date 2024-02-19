using System.Security.Claims;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IdentityAppdbContext context, UserManager<AppUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator, ILogger<AuthService> logger, IEmailService emailService,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
            _emailService = emailService;
            _roleManager = roleManager;
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

            var createToken = _jwtTokenGenerator.GeneratorToken(user);
            var userDto = new UserDto
            {
                ID = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
            };
            return new LoginResponseDto()
            {
                UserDto = userDto,
                Token = createToken,
            };
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            
            else
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }

                await _userManager.AddToRoleAsync(user, roleName);
                
                var claim = new Claim("Role", roleName);
                
                user.Role = roleName;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return false;
                }

                return true;
            }
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

        public async Task<bool> SendPasswordResetToken(string emailUser)
        {
            var user = await _userManager.FindByEmailAsync(emailUser);
            if (user == null)
            {
                return false;
            }

            var createToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var text = $"Token={createToken}";
            var email = new Email
            {
                Body = text,
                From = "amir.2002.ba@gmail.com",
                To = emailUser
            };
            await _emailService.SendEmail(email);
            return true;
        }

        public async Task<IdentityResult> RestPassword(RestPassword restPassword)
        {
            var user = await _userManager.FindByEmailAsync(restPassword.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "کاربر یافت نشد." });
            }

            if (restPassword.NewPassword != restPassword.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "پسورد با هم برابر نیست."
                });
            }

            var result = await _userManager.ResetPasswordAsync(user, restPassword.Token, restPassword.NewPassword);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError()
            {
                Description = "مشکل داره."
            });
        }

        public async Task<string> Register(RegisterModel register)
        {
            var appUser = new AppUser
            {
                Name = register.Name,
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                NormalizedEmail = register.Email.ToUpper(),
                Role = string.IsNullOrEmpty(register.Role) ? "V1" : register.Role,
            };

            try
            {
                var result = await _userManager.CreateAsync(appUser, register.Password);
                //_logger.LogInformation($"result : {result}");
                if (result.Succeeded)
                {
                    var user = new UserDto
                    {
                        ID = appUser.Id,
                        Name = appUser.Name,
                        Email = appUser.Email,
                        PhoneNumber = appUser.PhoneNumber,
                    };

                    var createConfirmationTokenAsync = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

                    var text = $"userId={appUser.Id}      Token={createConfirmationTokenAsync}";

                    var email = new Email
                    {
                        To = user.Email,
                        Body = text,
                        From = "amir.2002.ba@gmail.com",
                        Subject = "Active Email in API."
                    };

                    // await _emailService.SendEmail(email);

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

                return ex.Message;
            }
        }
    }
}