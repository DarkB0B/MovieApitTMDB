using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
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
        string apiKey = "2c3773c58b96fc195869c5f3162ff399";
        ExternalApiService externalApiService = new ExternalApiService();
        
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            int genreId = 14;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3//discover/movie?api_key={apiKey}&language=en-US\r\n&with_genres={genreId}&sort_by=vote_average.desc"),
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(body);
                
                List<Movie> movies = new List<Movie>();
                foreach (dynamic item in result.results)
                {
                    List<string> ids = new List<string>();
                    foreach (dynamic genre in item.genre_ids)
                    {
                        ids.Add(genre);
                    };
                    movies.Add(new Movie
                    {
                        Title = item.title,
                        Overview = item.overview,
                        PosterPath = item.poster_path,
                        ReleaseDate = item.release_date,
                        BackdropPath = item.backdrop_path,
                        OriginalTitle = item.original_title,
                        Popularity = item.popularity,
                        Id = item.id,   
                        GenreIds = ids

                    }) ; 
                }
                

                return new JsonResult(movies);
            }


        }
        
    }
}
