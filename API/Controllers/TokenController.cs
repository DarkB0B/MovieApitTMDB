using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult GetToken(UserCredentials user)
        {
            if (user != null && user.Password != null && user.UserName != null)
            {
                List<Claim> claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, user.UserName)
                };
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: creds);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                
                return new JsonResult(jwt);
            }
            return new JsonResult(BadRequest("Incorrect User Data"));

        }
        
        
    }
}
