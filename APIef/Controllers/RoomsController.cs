using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Data;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {

        private readonly DataContext _context;

        public RoomsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Post() //add room to db
        {
            Room room = new Room
            {
                Id = CodeGenerator.RandomString(5),
                UsersInRoom = 1,
                IsStarted = false,
                IsCompleted = false,
            };
            try
            {
                if(dbService.GetRoomFromDb(room.Id) == null)
                {
                    dbService.AddRoomToDb(room);
                    return Ok(room);
                }

                return BadRequest("Room With This Id Exists");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddUserToRoom")]
        public async Task<IActionResult> AddUserToRoom([FromBody] string Id) //add user to room
        {
            try
            {
                Room room = await dbService.GetRoomFromDb(Id);
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
        public async Task<IActionResult> AddMovieListToRoom([FromBody] List<Movie> movies, string Id) //if last user added a movie it returns "completed" so app can recognize when it ended
        {
            try
            {
                Room room = await dbService.GetRoomFromDb(Id);
                if (room.IsStarted == true && room.IsCompleted == false)
                {
                    room.MovieLists.Add(movies);
                    if (room.MovieLists.Count == room.UsersInRoom)
                    {                      
                        room.IsCompleted = true;
                        return  Ok("Picking Phase Completed");
                    }
                    dbService.UpdateRoomInDb(room);
                    return Ok("MovieList Updated");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("IsCompleted")]
        public async Task<JsonResult> IsCompleted(string Id) //check if picking phase is completed
        {
            Room room = await dbService.GetRoomFromDb(Id);
            if(room.IsCompleted == true)
            {
                return new JsonResult("true");
            }
            return new JsonResult("false");
        } 


        [HttpGet]
        public async Task<IActionResult> Get(string Id) //get room from db
        {
            try
            {
                Room room = await dbService.GetRoomFromDb(Id);
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
        public async Task<JsonResult> IsStarted(string Id)
        {
            Room room = await dbService.GetRoomFromDb(Id);
            if(room.IsStarted == true)
            {
                return new JsonResult("true");
            }
            return new JsonResult("false");
        }
        [HttpPut]
        [Route("StartRoom")]
        public async Task<IActionResult> StartRoom([FromBody] string Id) //start room
        {
            try
            {
                
                Room room = await dbService.GetRoomFromDb(Id);
                if (room.UsersInRoom >= 2)
                {
                    room.IsStarted = true;
                    dbService.UpdateRoomInDb(room);
                    return Ok(room);

                }
                return BadRequest("Not Enough People Joined The Room");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("MovieList")]
        public async Task<JsonResult> GetMovieList(string Id) 
        {
            Room room = await dbService.GetRoomFromDb(Id);
            double treshold = room.MovieLists.Count * 0.7;
            Dictionary<Movie, int> objCount = new Dictionary<Movie, int>();

            foreach(List<Movie> movieList in room.MovieLists)
            {
                foreach(Movie obj in movieList)
                {
                    if (objCount.ContainsKey(obj))
                    {
                        objCount[obj]++;
                    }
                    else
                    {
                        objCount.Add(obj, 1);
                    }
                }
            }
            List<Movie> commonMovies = new List<Movie>();
            foreach (KeyValuePair<Movie, int> entry in objCount)
            {
                if(entry.Value >= treshold)
                {
                    commonMovies.Add(entry.Key);
                }
            }
            return new JsonResult(Ok(commonMovies));
            
        }
    }
}
