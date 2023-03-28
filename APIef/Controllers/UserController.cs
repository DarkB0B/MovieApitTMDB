using APIef.Data;
using APIef.Models;
using APIef.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCredentials user)
        {
            if (await dbService.IsUsernameInDb(user.UserName) == true)
            {
                return BadRequest("Username Is Already Taken");
            }
            dbService.AddUserToDb(user);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            string res = await dbService.AreCredentialsOk(new UserCredentials { UserName = changePassword.UserName, Password = changePassword.Password});
            if (res == "ok")
            {
                dbService.UpdatePassword(new UserCredentials { UserName = changePassword.UserName, Password = changePassword.NewPassword});
                return Ok("Password Changed");
            }
            else
            {
                return BadRequest(res);
            }
        }
    }
}
