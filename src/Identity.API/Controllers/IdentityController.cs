using Identity.API.Model;
using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("/[Controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _responseDto;

        public IdentityController(IAuthService authService, ResponseDto responseDto)
        {
            _authService = authService;
            _responseDto = responseDto;
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
    }
}
