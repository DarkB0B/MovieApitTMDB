using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DbService dbService = new DbService();

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await dbService.IsUsernameInDb(user.UserName) == true)
            {
                return BadRequest("Username Is Already Taken");
            }
            dbService.AddUserToDb(user);
            return Ok();
        }
    }
}
