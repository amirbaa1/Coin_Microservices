using Identity.API.Model;
using Identity.API.Services;
using Identity.API.Services.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("/[Controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _responseDto;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<IdentityController> _logger;
        private readonly IEmailService _emailService;

        public IdentityController(IAuthService authService, ResponseDto responseDto, UserManager<AppUser> userManager,
            ILogger<IdentityController> logger, IEmailService emailService)
        {
            _authService = authService;
            _responseDto = responseDto;
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            var user = await _authService.Register(register);
            if (string.IsNullOrEmpty(user))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = user;
                return BadRequest(_responseDto);
            }

            _responseDto.Result = user;
            return Ok(_responseDto);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _authService.Login(login);
            if (user.UserDto == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "UserName and Password problem !!!";
                return BadRequest(_responseDto);
            }

            _responseDto.Result = user;
            return Ok(_responseDto);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordModel.UserName);
            if (user == null)
            {
                return NotFound("Not Found UserName");
            }

            // Ensure that the current password is correct before changing it
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, changePasswordModel.Password);
            if (!isCurrentPasswordValid)
            {
                // Handle the case where the current password is not valid
                return BadRequest();
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.Password,
                changePasswordModel.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Change Password.");
            }

            return BadRequest();
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmail confirmEmail)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmail.Email);

            if (user == null)
            {
                return NotFound("NotFound Email.");
            }

            _logger.LogInformation($"userID : {user.Id}");


            var result = await _userManager.ConfirmEmailAsync(user, confirmEmail.Token);
            _logger.LogInformation($"Token :{confirmEmail.Token}");
            _logger.LogInformation($"Result : {result}");

            if (!result.Succeeded)
            {
                return BadRequest("No active Email.");
            }

            // تایید ایمیل با موفقیت انجام شده است
            return Ok($"Email confirmed successfully, {user}");
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await _authService.SendPasswordResetToken(email);
            if (user == null)
            {
                return NotFound("Not Found Email.");
            }

            return Ok($"send Token Change Password in {email}");
        }
        [HttpPost("RestPassword")]
        public async Task<IActionResult> RestPassword(RestPassword restPassword)
        {
            var user = await _authService.RestPassword(restPassword);
            if (user == null)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }
    }
}