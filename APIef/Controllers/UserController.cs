using APIef.Data;
using APIef.Interface;
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
        private readonly IUsers _IUser;

        public UserController(IUsers IUser )
        {
            _IUser = IUser;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCredentials user)
        {
            if (await Task.FromResult(_IUser.UserExists(user.UserName) == true)) 
            {
                return BadRequest("Username Is Already Taken");
            }
            _IUser.AddUser(new User {UserName = user.UserName, Password = user.Password });
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            string res = await Task.FromResult(_IUser.CheckCredentials(new UserCredentials { UserName = changePassword.UserName, Password = changePassword.Password}));
            if (res == "OK")
            {
                _IUser.ChangePassword(new UserCredentials { UserName = changePassword.UserName, Password = changePassword.NewPassword});
                return Ok("Password Changed");
            }
            else
            {
                return BadRequest(res);
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetUser(string userName)
        {
            User user = await Task.FromResult(_IUser.GetUser(userName));
            return new JsonResult(user);
        }
    }
}
