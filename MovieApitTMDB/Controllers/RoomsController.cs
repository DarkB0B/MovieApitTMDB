using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApitTMDB.Models;
using MovieApitTMDB.Services;

namespace MovieApitTMDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        readonly DbService dbService = new DbService();

        [HttpPost]
        public IActionResult Post([FromBody] Room room)
        {
            try
            {
                dbService.AddRoomToDb(room);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddUserToRoom")]
        public IActionResult AddUserToRoom([FromBody] string Id)
        {
            try
            {
                Room room = dbService.GetRoomFromDb(Id);
                room.UsersInRoom++;
                dbService.UpdateRoomInDb(room);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("AddMovieListToRoom")]
        public IActionResult AddMovieListToRoom([FromBody] string Id, List<Movie> movies) //if last user added a movie it returns "completed" so app can recognize when it ended
        {
            try
            {
                Room room = dbService.GetRoomFromDb(Id);
                room.MovieLists.Add(movies);
                if(room.MovieLists.Count == room.RoomSize)
                {
                    string completed = "completed";
                    room.IsCompleted = true;
                    return new JsonResult(completed);
                }
                dbService.UpdateRoomInDb(room);
                return Ok("MovieList Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("IsCompleted")]
        public JsonResult IsCompleted(string Id)
        {
            Room room = dbService.GetRoomFromDb(Id);
            if(room.IsCompleted == true)
            {
                return new JsonResult("true");
            }
            return new JsonResult("false");
        }

  


        [HttpGet]
        public IActionResult Get(string Id)
        {
            try
            {
                Room room = dbService.GetRoomFromDb(Id);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] Room room)
        {
            try
            {
                dbService.UpdateRoomInDb(room);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
