﻿namespace Identity.API.Model
{
    public class LoginResponseDto
    {
        public UserDto UserDto { get; set; }
        public string Token { get; set; }
    }
}
