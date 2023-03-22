using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MovieApitTMDB.Models;
using MovieApitTMDB.Services;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieApitTMDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        
        readonly ExternalApiService externalApiService = new ExternalApiService();
        readonly DbService dbService = new DbService();

        [HttpGet]
        public async Task<JsonResult> Get(int genreId)
        {
            List<Movie> movies = await externalApiService.GetMoviesPerGenre(genreId);
            MovieCollection moviesCollection = new MovieCollection
            {
                Id = 1,
                Title = "Test",
                Movies = movies
            };
            
            return new JsonResult(movies);
        }

        [HttpGet]
        [Route("GetMovieCollection")]      
        public async Task<JsonResult> GetMovieCollection(int collectionId)
        {
            MovieCollection movieCollection = dbService.GetMovieCollectionFromDb(collectionId);
            return new JsonResult(movieCollection);


            
        }
    }
}
