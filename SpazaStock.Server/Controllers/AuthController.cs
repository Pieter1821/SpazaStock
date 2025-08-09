using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpazaStock.Server.DTOs;
using SpazaStock.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpazaStock.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SpazaStockDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(SpazaStockDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Username already exists.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                SmsEnabled = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized();

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("mobile", user.MobileNumber)
            };
            var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
