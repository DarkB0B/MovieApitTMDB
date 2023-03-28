﻿using APIef.Models;
using APIef.Repository;
using APIef.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        private readonly IUsers _IUser;

        public TokenController(IConfiguration configuration, IUsers IUser) 
        {
            _configuration = configuration;
            _IUser = IUser;
        }

        [HttpPost]
        public JsonResult GetToken(UserCredentials userCredentials)
        {
            if (userCredentials != null && userCredentials.Password != null && userCredentials.UserName != null)
            {
                User user =_IUser.GetUser(userCredentials.UserName);
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
                
                return new JsonResult(jwt);
            }
            return new JsonResult(BadRequest("Incorrect User Data"));

        }
        
        
    }
}