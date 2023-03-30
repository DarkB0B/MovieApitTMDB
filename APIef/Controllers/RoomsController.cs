
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Data;
using APIef.Interface;

namespace APIef.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRooms _roomService;

        public RoomsController(IRooms roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                Room room = await _roomService.GetRoomAsync(id);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Room room)
        {
            try
            {
                if (id != room.Id)
                {
                    return BadRequest("Room id does not match request id");
                }

                await _roomService.UpdateRoomAsync(room);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/IsStarted")]
        public async Task<IActionResult> IsStarted(string id)
        {
            Room room = await _roomService.GetRoomAsync(id);
            if (room.IsStarted)
            {
                return Ok(true);
            }

            return Ok(false);
        }
        //This method starts room when there are at least 2 people connected
        [HttpPut("{id}/StartRoom")]
        public async Task<IActionResult> StartRoom(string id)
        {
            try
            {
                Room room = await _roomService.GetRoomAsync(id);
                if (room.UsersInRoom >= 2)
                {
                    room.IsStarted = true;
                    await _roomService.UpdateRoomAsync(room);
                    return Ok(room);
                }

                return BadRequest("Not Enough People Joined The Room");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        //This method is called when the room is completed and returns list of movies that are common to all movie lists
        [HttpGet("{id}/MovieList")]
        public async Task<IActionResult> GetMovieList(string id)
        {
            Room room = await _roomService.GetRoomAsync(id);
            double threshold = room.MovieLists.Count * 0.7;
            Dictionary<Movie, int> objCount = new Dictionary<Movie, int>();

            foreach (MovieList movieList in room.MovieLists)
            {
                foreach (Movie obj in movieList.Movies)
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
                if (entry.Value >= threshold)
                {
                    commonMovies.Add(entry.Key);
                }
            }

            return Ok(commonMovies);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
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
                while(_roomService.RoomExists(room.Id) == true)
                {
                    room.Id = CodeGenerator.RandomString(5);
                }
             
                await _roomService.AddRoomAsync(room);
                return Ok(room);   
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/AddUserToRoom")]
        public async Task<IActionResult> AddUserToRoom(string id)
        {
            try
            {
                Room room = await _roomService.GetRoomAsync(id);
                room.UsersInRoom++;
                 _roomService.UpdateRoom(room);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/RemoveUserFromRoom")]
        public async Task<IActionResult> RemoveUserFromRoom(string id)
        {
            try
            {
                Room room = await _roomService.GetRoomAsync(id);
                room.UsersInRoom--;
                _roomService.UpdateRoom(room);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //This method adds movie list to room and if all users have added their movie list, it sets the room to completed
        [HttpPost("{id}/movieLists")]
        public async Task<IActionResult> AddMovieListToRoom(string id, [FromBody] List<Movie> movies) //add movie list to room
        {
            try
            {
                Room room = await _roomService.GetRoomAsync(id);
                if (room.IsStarted && !room.IsCompleted)
                {
                    room.MovieLists.Add(new MovieList { Id = room.MovieLists.Count + 1, Movies = movies });
                    if (room.MovieLists.Count == room.UsersInRoom)
                    {
                        room.IsCompleted = true;
                        return Ok("Picking Phase Completed");
                    }
                    await _roomService.UpdateRoomAsync(room);
                    return Ok("MovieList Updated");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //check if picking phase is completed
        [HttpGet("{id}/isCompleted")]
        public async Task<JsonResult> IsCompleted(string id) 
        {
            Room room = await _roomService.GetRoomAsync(id);
            if (room.IsCompleted)
            {
                return new JsonResult("true");
            }
            return new JsonResult("false");
        }
    }
}