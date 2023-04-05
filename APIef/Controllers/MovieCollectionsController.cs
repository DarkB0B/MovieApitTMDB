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
        [HttpPost]
        [Route("SaveCollectionInDb")]
        public async Task<IActionResult> Post([FromBody] MovieCollection movieCollection)
        {
            movieCollection.Id = 0;
            List<Movie> movies = new List<Movie>();
            foreach (Movie movie in movieCollection.Movies) { movies.Add(movie); };


            MovieCollection movieCollection2 = _movieCollectionsService.AddMovieListToCollection(movieCollection, movies);
            await _movieCollectionsService.AddMovieCollectionAsync(movieCollection2);

            return Ok();
        }

       
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MovieCollection movieCollection)
        {
            await _movieCollectionsService.UpdateMovieCollectionAsync(movieCollection);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieCollectionsService.DeleteMovieCollectionAsync(id);
            return Ok();
        }
    }
}

