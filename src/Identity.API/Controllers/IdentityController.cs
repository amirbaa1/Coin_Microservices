using Identity.API.Model;
using Identity.API.Services;
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

        public IdentityController(IAuthService authService, ResponseDto responseDto, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _responseDto = responseDto;
            _userManager = userManager;
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
        
        [HttpPost("ActivateEmail")]
        public async Task<IActionResult> ConfirmEmail()
        {
            return Ok();
        }
    }
}