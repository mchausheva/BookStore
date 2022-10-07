using BookStore.BL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;
        public IdentityController(IConfiguration configuration, IEmployeeService employeeService)
        {
            _configuration = configuration;
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserInfo userData)
        {
            if (userData != null && !string.IsNullOrEmpty(userData.Email) && !string.IsNullOrEmpty(userData.Password))
            {
                var user = await _employeeService.GetUserInfoAsync(userData.Email, userData.Password);

                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt:Subject").Value),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.DisplayName ?? string.Empty),
                        new Claim("UserName", user.UserName ?? string.Empty),
                        new Claim("Email", user.Email ?? string.Empty),
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                                     _configuration["Jwt:Audience"],
                                                     claims,
                                                     expires: DateTime.UtcNow.AddMinutes(10),
                                                     signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Missing username and/or password");
            }
        }
    }
}
