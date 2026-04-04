using AutoAlertBackEnd.Dtos;
using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoAlertBackEnd.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly IConfiguration _configuration;
        public AuthController(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
             IConfiguration configuration
        )
        { 
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }

        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn(LogInDto logIn)
        {
            if (logIn == null || string.IsNullOrEmpty(logIn.Email) || string.IsNullOrEmpty(logIn.Password))
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userRepository.GetUserByEmailAsync(logIn.Email);
            if (user == null) {
                return Unauthorized();
            }
            if (BCrypt.Net.BCrypt.Verify(logIn.Password, user.PasswordHash))
            {
                var token = GenerateJwtForUser(user);
                Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    // Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
                return Ok();
            }

            return Unauthorized();
        }

        private string GenerateJwtForUser(Users user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key missing");
            var issuer = _configuration["Jwt:Issuer"] ?? _configuration["Jwt:Iusser"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(jwtKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Names)
            };
            if (user.RoleId.HasValue)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.RoleId.Value.ToString()));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
