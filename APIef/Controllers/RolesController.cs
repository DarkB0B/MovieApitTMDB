using APIef.Data;
using APIef.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIef.Controllers
{
    [Authorize(Roles = "Admin, Regular")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public RolesController(DataContext context)
        {
            _dbContext = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddRole([FromBody] string role)
        {
            _dbContext.Roles.AddAsync(new Role { Name = role, RoleId = 0 });
            _dbContext.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok(_dbContext.Roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _dbContext.Roles.FirstOrDefault(r => r.RoleId == id);
            _dbContext.Roles.Remove(role);
            _dbContext.SaveChanges();
            return Ok();
        }

       
    }
}
