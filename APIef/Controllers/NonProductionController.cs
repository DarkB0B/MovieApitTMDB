using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonProductionController : ControllerBase
    {

        readonly ExternalApiService externalApiService = new ExternalApiService();
        readonly DbService dbService = new DbService();

        
        [HttpPut]
        [Route("SaveGenresInDb")]
        public async Task<IActionResult> SaveGenresInDb()
        {
            List<Genre> genres = new List<Genre>();
            genres = externalApiService.GetGenres().Result;

            try
            {
                genres.ForEach(genre => { dbService.AddGenreToDb(genre); });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        
        
    }
}
