using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApitTMDB.Models;
using MovieApitTMDB.Services;

namespace MovieApitTMDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonProductionController : ControllerBase
    {

        ExternalApiService externalApiService = new ExternalApiService();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "BpCO4ueDkrgbvbsW7Qgsm6HOMyLgwqXVmQYB0QfP\r\n",
            BasePath = "https://shrex-fa4af-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client;

        [HttpPut]
        [Route("SaveGenresInDb")]
        public async Task<IActionResult> SaveGenresInDb()
        {
            List<Genre> genres = new List<Genre>();
            genres = externalApiService.GetGenres().Result;

            try
            {
                genres.ForEach(genre => { AddGenresToDb(genre); });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        private void AddGenresToDb(Genre genre)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = genre;
            //PushResponse response = client.Push("Genres/",genre);
            SetResponse setResponse = client.Set("Genres/" + genre.Id, genre);
        }
        
    }
}
