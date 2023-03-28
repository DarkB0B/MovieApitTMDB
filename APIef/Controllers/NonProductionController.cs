using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Data;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonProductionController : ControllerBase
    {

        readonly ExternalApiService externalApiService = new ExternalApiService();
        private readonly DataContext _context;

        public NonProductionController(DataContext context)
        {
            _context = context;
        }

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
