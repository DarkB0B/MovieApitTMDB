using APIef.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Interface;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCollectionsController : ControllerBase
    {
        private readonly IMovieCollections _movieCollectionsService;

        public MovieCollectionsController(IMovieCollections movieCollectionsService)
        {
            _movieCollectionsService = movieCollectionsService;
        }

        // GET api/movie-collections/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                MovieCollection movieCollection = await Task.FromResult(_movieCollectionsService.GetMovieCollection(id));

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
                List<MovieCollection> movieCollections = await Task.FromResult(_movieCollectionsService.GetMovieCollections());

                return Ok(movieCollections);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        // POST api/movie-collections
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieCollection movieCollection)
        {
            await _movieCollectionsService.AddMovieCollectionAsync(movieCollection);
            return Ok();
        }
    }
}

