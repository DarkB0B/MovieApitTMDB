using APIef.Data;
using APIef.Interface;
using APIef.Models;
using APIef.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIef.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsers _userService;
        private readonly DataContext _dbContext;

        public TokensController(IConfiguration configuration, IUsers userService, DataContext dbContext)
        {
            _configuration = configuration;
            _userService = userService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult CreateToken([FromBody] UserCredentials userCredentials)
        {
            if (userCredentials == null)
            {
                return BadRequest("User credentials are required.");
            }

            if (string.IsNullOrEmpty(userCredentials.UserName))
            {
                return BadRequest("Username is required.");
            }

            if (string.IsNullOrEmpty(userCredentials.Password))
            {
                return BadRequest("Password is required.");
            }

            User user = _userService.GetUser(userCredentials.UserName);

            if (user.Role == null)
            {
                return BadRequest("Incorrect user data.");
            }

            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwt);
        }
    }
}
