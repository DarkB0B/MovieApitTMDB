using APIef.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCollectionsController : ControllerBase
    {
        private readonly DataContext _context;

        public MovieCollectionsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> GetMovieCollection(int collectionId)
        {
            try
            {
                MovieCollection movieCollection = await dbService.GetMovieCollectionFromDb(collectionId);
                return new JsonResult(movieCollection);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }
        [HttpGet]
        [Route("All")]
        public async Task<JsonResult> GetMovieCollections()
        {
            try
            {
                List<MovieCollection> movieCollectionList = await dbService.GetAllMovieCollectionsFromDb();
                return new JsonResult(movieCollectionList);
            }
            catch (Exception ex)
            {
                return new JsonResult($"{ex.Message}");
            }
        }
    }
}
