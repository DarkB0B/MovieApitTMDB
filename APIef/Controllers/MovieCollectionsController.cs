using APIef.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Interface;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCollectionsController : ControllerBase
    {
        private readonly IMovieCollections _IMovieCollections;

        public MovieCollectionsController(IMovieCollections IMovieCollection)
        {
            _IMovieCollections = IMovieCollection;
        }

        [HttpGet]
        public async Task<JsonResult> GetMovieCollection(int collectionId)
        {
            try
            {
                MovieCollection movieCollection = await Task.FromResult(_IMovieCollections.GetMovieCollection(collectionId));
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
                List<MovieCollection> movieCollectionList = await Task.FromResult(_IMovieCollections.GetMovieCollections());
                return new JsonResult(movieCollectionList);
            }
            catch (Exception ex)
            {
                return new JsonResult($"{ex.Message}");
            }
        }
    }
}
