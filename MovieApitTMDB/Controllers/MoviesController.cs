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

        
        [HttpGet]
        public async Task<JsonResult> Get(int genreId)
        {
            List<Movie> movies = await externalApiService.GetMoviesPerGenre(genreId);
            return new JsonResult(movies);
        }       
    }
}
