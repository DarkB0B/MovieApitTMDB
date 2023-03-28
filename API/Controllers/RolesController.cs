using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        readonly DbService dbService = new DbService();

        [HttpGet]
        public IActionResult Get()
        {

            return Ok("Get");
        }
        [HttpGet]
        [Route("All")]
        public IActionResult Get()
        {

            return Ok("Get");
        }
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Post");
        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Put");
        }
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Delete");
        }
    }
}
