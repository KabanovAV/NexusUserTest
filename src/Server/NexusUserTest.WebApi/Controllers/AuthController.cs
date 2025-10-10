using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexusUserTest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IRepoServiceManager service, IConfiguration config) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;
        private readonly IConfiguration _config = config;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _service.UserRepository.GetUserAsync(u => u.Login == dto.UserLogin && u.Password == dto.Password);
            if (user == null)
                return Unauthorized(new { message = "Введен неверный логин или пароль" });

            var token = GenerateJwtToken(new LoginDto { Id = user.Id, UserLogin = user.Login, Password = user.Password });
            return Ok(new { token });
        }

        private string GenerateJwtToken(LoginDto user)
        {
            var claims = new List<Claim>
            {
                new ("UserId", user.Id.ToString()),
                new (ClaimTypes.Name, user.UserLogin!),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
