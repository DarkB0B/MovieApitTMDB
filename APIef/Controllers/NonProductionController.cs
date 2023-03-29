
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Data;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonProductionController : ControllerBase
    {
        ExternalApiService externalApiService = new ExternalApiService();
        DataContext _dbContext;

        public NonProductionController (DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetGenres")]
        public async Task<JsonResult> GetGenres()
        {
            List<Genre> genres = await externalApiService.GetGenres();
            return new JsonResult(genres);
        }
        [HttpGet]
        [Route("SaveGenres")]
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
        [HttpPost]
        [Route("SaveRoles")]
        public IActionResult PostGenres()
        {
            List<Role> roles = new List<Role>();
            Role r1 = new Role { Name = "Regular", Id = 0 };
            Role r2 = new Role { Name = "Premium", Id = 0 };
            Role r3 = new Role { Name = "Admin", Id = 0 };
            roles.Add(r1);
            roles.Add(r2);
            roles.Add(r3);
            foreach (var role in roles)
            {
                _dbContext.Roles.Add(role);
            }
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("GetUsers")]
        public JsonResult GetUsers() 
        {
            List<User> users = _dbContext.Users.ToList();
            return new JsonResult(users);
        }
        [HttpGet]
        [Route("CreateMovieCollecitionAndSaveInDb")]
        public async Task<JsonResult> Get()
        {
            int genre = 14;
            List<Movie> movies = new List<Movie>();

            movies = await externalApiService.GetMoviesPerGenre(genre);

            MovieCollection movieCollection = new MovieCollection();
            movieCollection.Movies = movies;
            movieCollection.Title = "Action";
            movieCollection.Id = 0;
            _dbContext.MovieCollections.Add(movieCollection);
            _dbContext.SaveChanges();

                        
            return new JsonResult(movieCollection);
        }
        

    }
}
