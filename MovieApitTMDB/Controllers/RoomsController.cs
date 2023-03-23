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
        public IActionResult AssUserToRoom([FromBody] string userId, string Id)
        {
            try
            {
                Room room = dbService.GetRoomFromDb(Id);
                room.UserIdList.Add(userId);
                dbService.UpdateRoomInDb(room);
                return Ok($"User {Id} joined room {room.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
