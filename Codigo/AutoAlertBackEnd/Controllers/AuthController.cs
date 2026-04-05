using AutoAlertBackEnd.Dtos;
using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IRoleSubModuleRepository _roleSubModuleRepository;
        private readonly IConfiguration _configuration;
        public AuthController(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IRoleSubModuleRepository roleSubModuleRepository,
            IConfiguration configuration
        )
        { 
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _roleSubModuleRepository = roleSubModuleRepository;
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

        // Endpoint to get current user info
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            // Get user ID from JWT claims and verify user exists in database
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            
            // Fetch user from database and verify role and permissions
            var user = await _userRepository.GetUserByIdAsync(Guid.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }

            var role = await _roleRepository.GetPermissionByUserAsync(user);

            // Get the expiration time of the JWT token and calculate remaining time
            var expClaim = User.FindFirst("exp")?.Value;
            var exp = long.Parse(expClaim ?? "0");
            var expires = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;

            return Ok(new
            {
                User = new {
                    Id =  user.Id,
                    Names = user.Names,
                    LastNames = user.LastNames,
                    Email = user.Email,
                    Role = role.Role,
                    Permissions = role.SpecialPermissions
                },
                ExpiresIn = (int)(expires - DateTime.UtcNow).TotalSeconds,
            });
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
            if (user.RoleId != Guid.Empty)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.RoleId.ToString()));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
