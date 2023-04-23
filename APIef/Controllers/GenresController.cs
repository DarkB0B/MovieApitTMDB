using APIef.Data;
using APIef.Models;
using APIef.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace APIef.Controllers
{
    [Authorize(Roles = "Regular, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
     
        private readonly DataContext _dbContext;
        public GenresController(DataContext context)
        {
            _dbContext = context;
        }

        
        [HttpGet]
        public async Task<JsonResult> GetGenres()
        {
            return new JsonResult(Ok(await Task.FromResult(_dbContext.Genres.ToList())));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> AddGenre([FromBody] Genre genre)
        {
            await _dbContext.Genres.AddAsync(genre);
            await _dbContext.SaveChangesAsync();
            return new JsonResult(Ok());
        }

        

    }   
}
