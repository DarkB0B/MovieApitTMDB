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
        public async Task<JsonResult> Get([FromBody] List<int> genreList)
        {
            
            List<Movie> movies = new List<Movie>();
            foreach (int genreId in genreList)
            {
                movies = await externalApiService.GetMoviesPerGenre(genreId);               
            }
            //randomly choose some movies from movies list                 
            return new JsonResult(movies);
        }
        [HttpGet]
        [Route("GetMoviePerGenreTest")]
        public async Task<JsonResult> Get(int genre)
        {

            List<Movie> movies = new List<Movie>();
           
                movies = await externalApiService.GetMoviesPerGenre(genre);
         
            //randomly choose some movies from movies list                 
            return new JsonResult(movies);
        }

    }
}
