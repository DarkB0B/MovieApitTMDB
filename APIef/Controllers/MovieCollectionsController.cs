using APIef.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Interface;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace APIef.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCollectionsController : ControllerBase
    {
        private readonly IMovieCollections _movieCollectionsService;
        private readonly DataContext _context;
        readonly ExternalApiService externalApiService = new ExternalApiService();
        public MovieCollectionsController(IMovieCollections movieCollectionsService, DataContext context)
        {
            _movieCollectionsService = movieCollectionsService;
            _context = context;
        }

        // GET api/movie-collections/{id}
        [Authorize(Roles = "Regular")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                MovieCollection movieCollection = await _movieCollectionsService.GetMovieCollectionAsync(id);

                if (movieCollection == null)
                {
                    return NotFound();
                }
               

                return Ok(movieCollection);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/movie-collections
        [Authorize(Roles = "Regular")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<MovieCollection> movieCollections = await _movieCollectionsService.GetMovieCollectionsAsync();
                Console.WriteLine("sent some data");
                return Ok(movieCollections);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        // POST api/movie-collections
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<String> movieIds, string title, string description)
        {
            List<Movie> movies = await externalApiService.GetMoviesById(movieIds);
            MovieCollection movieCollection = new MovieCollection();
            movieCollection.Title = title;
            movieCollection.Description = description;
            movieCollection.Id = 0;
            MovieCollection movieCollection2 = _movieCollectionsService.AddMovieListToCollection(movieCollection, movies);
            await _movieCollectionsService.AddMovieCollectionAsync(movieCollection2);
            
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("test/SaveCollectionInDbFromWeb")]
        public async Task<IActionResult> Postsave(int id)
        {
            MovieCollection movieCollection = new MovieCollection();
            List<Movie> movies = await externalApiService.GetMoviesPerGenre(id,1,true);
            movieCollection.Id = 0;
            movieCollection.Title = "test";
            movieCollection.Description = "test";
            MovieCollection movieCollection2 = _movieCollectionsService.AddMovieListToCollection(movieCollection, movies);
            await _movieCollectionsService.AddMovieCollectionAsync(movieCollection2);

            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("test/SaveCollectionInDb")]
        public async Task<IActionResult> Post([FromBody] MovieCollection movieCollection)
        {
            movieCollection.Id = 0;
            List<Movie> movies = new List<Movie>();
            foreach (Movie movie in movieCollection.Movies) { movies.Add(movie); };


            MovieCollection movieCollection2 = _movieCollectionsService.AddMovieListToCollection(movieCollection, movies);
            await _movieCollectionsService.AddMovieCollectionAsync(movieCollection2);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MovieCollection movieCollection)
        {
            await _movieCollectionsService.UpdateMovieCollectionAsync(movieCollection);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieCollectionsService.DeleteMovieCollectionAsync(id);
            return Ok();
        }
    }
}

