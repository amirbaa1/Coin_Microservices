using Identity.API.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOption _jwtOption;

        public JwtTokenGenerator(IOptions<JwtOption> jwtOption)
        {
            _jwtOption = jwtOption.Value;
        }

        public string GeneratorToken(AppUser appUser)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOption.Secret);

            var claim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, appUser.Name),
                new Claim(JwtRegisteredClaimNames.Name,appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Id),

            };

            var TokenDescription = new SecurityTokenDescriptor
            {
                Audience = _jwtOption.Audience,
                Issuer = _jwtOption.Issuer,
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var Token = TokenHandler.CreateToken(TokenDescription);
            return TokenHandler.WriteToken(Token);
        }
    }
}
