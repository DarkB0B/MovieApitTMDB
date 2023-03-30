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
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
            return new JsonResult(Ok());
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<JsonResult> GetAllGenres()
        {
            List<Genre> genres = await Task.FromResult(_dbContext.Genres.ToList());
            return new JsonResult(genres);
        }
        [HttpPost]
        [Route("SaveAll")]
        public async Task<JsonResult> SaveGenres()
        {

            List<Genre> genres = await externalApiService.GetGenres();
            foreach (var genre in genres)
            {
                _dbContext.Genres.Add(genre);
            }
            _dbContext.SaveChanges();
            return new JsonResult(genres);
        }

    }   
}
