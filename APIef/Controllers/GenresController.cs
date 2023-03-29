using APIef.Data;
using APIef.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIef.Controllers
{
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
            return new JsonResult(Ok(_dbContext.Genres));
        }
        [HttpPost]
        public async Task<JsonResult> AddGenre([FromBody] Genre genre)
        {
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
            return new JsonResult(Ok());
        }
        
    }   
}
