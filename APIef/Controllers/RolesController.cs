using APIef.Data;
using APIef.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;

        public RolesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddRole([FromBody] string role)
        {
            _context.Roles.Add(new Role { Name = role, RoleId = 0 });
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok(_context.Roles);
        }
    }
}
