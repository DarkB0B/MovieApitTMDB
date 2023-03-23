using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApitTMDB.Models;
using MovieApitTMDB.Services;

namespace MovieApitTMDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCollectionsController : ControllerBase
    {
        readonly DbService dbService = new DbService();

        [HttpGet]
        public async Task<JsonResult> GetMovieCollection(int collectionId)
        {
            try
            {
                MovieCollection movieCollection = dbService.GetMovieCollectionFromDb(collectionId);
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
                List<MovieCollection> movieCollectionList = dbService.GetAllMovieCollectionsFromDb();
                return new JsonResult(movieCollectionList);
            }
            catch (Exception ex)
            {
                return new JsonResult($"{ex.Message}");
            }
        }
    }
}
