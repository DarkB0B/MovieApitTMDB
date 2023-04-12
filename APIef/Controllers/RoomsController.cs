
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
        private readonly DataContext _context;
        private readonly IMovies _movieService;
        public RoomsController(IRooms roomService, DataContext context, IMovies movieService)
        {
            _movieService = movieService;
            _roomService = roomService;
            _context = context;
        }
        //get room by id
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
        //edit room
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
      
        //This method starts room when there are at least 2 people connected
        [HttpPatch("{id}")]
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
        
        
        //create new room
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
                while(await _roomService.RoomExistsAsync(room.Id) == true)
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

        [HttpPatch("{id}/users")]
        public async Task<IActionResult> AddUserToRoom(string id, string option)
        {
            try
            {
                if (option == "add")
                {
                    Room room = await _roomService.GetRoomAsync(id);
                    room.UsersInRoom++;
                    await _roomService.UpdateRoomAsync(room);
                    return Ok();
                }
                else if (option == "remove")
                {
                    Room room = await _roomService.GetRoomAsync(id);
                    room.UsersInRoom--;
                    await _roomService.UpdateRoomAsync(room);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        //This method adds movie list to room and if all users have added their movie list, it sets the room to completed and generates final movie list
        [HttpPost("{id}/movielists")]
        public async Task<IActionResult> AddMovieListToRoom(string id, [FromBody] List<Movie> movies) //add movie list to room
        {  
            try
            {
                Room room = await _roomService.GetRoomAsync(id);
                string listId = room.Id + room.MovieLists.Count.ToString();
                List<Movie> movieList = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    Movie? x = await _movieService.GetMovieAsync(movie.Id);
                    if(x == null)
                    {
                        movieList.Add(movie);
                    }
                    else if(x != null)
                    {
                        movieList.Add(x);
                    }
                }
                MovieList list = new MovieList { Movies = movieList, Id = listId };
                await _context.MovieLists.AddAsync(list);
                await _context.SaveChangesAsync();
                
                if (room.IsStarted && !room.IsCompleted) 
                {
                    await _roomService.AddListToRoomAsync(id , list);

                    if (room.MovieLists.Count == room.UsersInRoom)
                    {
                        room.IsCompleted = true;
                        MovieList finalList = _roomService.GetFinalList(room);
                        await _context.MovieLists.AddAsync(finalList);
                        await _roomService.UpdateRoomAsync(room);
                        await _context.SaveChangesAsync();
                        await _roomService.AddListToRoomAsync(id, finalList);
                        await _context.SaveChangesAsync();
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
        
        //delete room and asociated lists
        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(string id)
        {
            Room room = await _roomService.GetRoomAsync(id);
            foreach(MovieList list in room.MovieLists)
            {
                _context.MovieLists.Remove(list);
            }
            await _context.SaveChangesAsync();
            await _roomService.DeleteRoomAsync(id);
            return Ok();
        }
    }
}