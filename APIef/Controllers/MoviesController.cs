
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using APIef.Data;
using APIef.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace APIef.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {


        readonly ExternalApiService externalApiService = new ExternalApiService();
        private readonly IMovies _movieService;

        public MoviesController(IMovies movieService)
        {
            _movieService = movieService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> Get([FromBody] List<int> genreList, int requestedCount)
        {
            List<Movie> result = new List<Movie>();

            // Determine how many movies to get for each genre based on the requested count
            int moviesPerGenre = requestedCount / genreList.Count;

            foreach (int genreId in genreList)
            {
                // Get 12 movies for the current genre
                List<Movie> moviesForGenre = await externalApiService.GetMoviesPerGenre(genreId, 1);

                // Add up to `moviesPerGenre` movies from the current genre to the result list
                int moviesAdded = 0;
                foreach (Movie movie in moviesForGenre)
                {
                        
                    result.Add(movie);
                    moviesAdded++;

                    if (moviesAdded >= moviesPerGenre)
                    {
                       break;
                    }
                    
                }

                // If we couldn't find enough movies for the current genre, add as many as we can
                while (moviesAdded < moviesPerGenre && moviesForGenre.Any())
                {
                    Movie movieToAdd = moviesForGenre.First();
                    moviesForGenre.RemoveAt(0);

                    
                     result.Add(movieToAdd);
                     moviesAdded++;

                }

                // If we still don't have enough movies, break out of the loop
                if (result.Count >= requestedCount)
                {
                    break;
                }
            }

            return new JsonResult(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("test/GetMoviePerGenre")]
        public async Task<JsonResult> Get(int genre)
        {

            List<Movie> movies = new List<Movie>();
           
                movies = await externalApiService.GetMoviesPerGenre(genre, 1);
         
            //randomly choose some movies from movies list                 
            return new JsonResult(movies);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("test/GetMoviesFromDb")]
        public async Task<JsonResult> GetMoviesFromDb()
        {
            List<Movie> movies = new List<Movie>();
            movies = await _movieService.GetMoviesAsync();
            return new JsonResult(movies);
        }

    }
}
