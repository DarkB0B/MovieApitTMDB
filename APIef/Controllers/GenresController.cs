using APIef.Data;
using APIef.Models;
using APIef.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        readonly ExternalApiService externalApiService = new ExternalApiService();
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
        [HttpPost]
        public async Task<JsonResult> AddGenre([FromBody] Genre genre)
        {
            await _dbContext.Genres.AddAsync(genre);
            await _dbContext.SaveChangesAsync();
            return new JsonResult(Ok());
        } 
        [HttpPost]
        [Route("test/SaveAll")]
        public async Task<JsonResult> SaveGenres()
        {

            List<Genre> genres = await externalApiService.GetGenres();
            foreach (var genre in genres)
            {
                await _dbContext.Genres.AddAsync(genre);
            }
            await _dbContext.SaveChangesAsync();
            return new JsonResult(genres);
        }

    }   
}
