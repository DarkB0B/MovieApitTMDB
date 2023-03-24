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
        public IActionResult Post([FromBody] Room room) //add room to db
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
        public IActionResult AddUserToRoom([FromBody] string Id) //add user to room
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
        public IActionResult AddMovieListToRoom([FromBody] List<Movie> movies, string Id) //if last user added a movie it returns "completed" so app can recognize when it ended
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
        public JsonResult IsCompleted(string Id) //check if picking phase is completed
        {
            Room room = dbService.GetRoomFromDb(Id);
            if(room.IsCompleted == true)
            {
                return new JsonResult("true");
            }
            return new JsonResult("false");
        } 


        [HttpGet]
        public IActionResult Get(string Id) //get room from db
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
        public IActionResult Put([FromBody] Room room) //update room in db
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
        [HttpGet]
        [Route("IsStarted")] //check if room is started
        public JsonResult IsStarted(string Id)
        {
            Room room = dbService.GetRoomFromDb(Id);
            if(room.IsStarted == true)
            {
                return new JsonResult("true");
            }
            return new JsonResult("false");
        }
        [HttpPut]
        [Route("StartRoom")]
        public IActionResult StartRoom([FromBody] string Id) //start room
        {
            try
            {
                Room room = dbService.GetRoomFromDb(Id);
                room.IsStarted = true;
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
