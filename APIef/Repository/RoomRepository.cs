using APIef.Data;
using APIef.Models;
using Microsoft.EntityFrameworkCore;

namespace APIef.Repository
{
    public class RoomRepository : IRooms
    {
        readonly DataContext _context;
        public RoomRepository(DataContext context)
        {
            _context = context;
        }

        public void AddRoom(Room room)
        {
            try
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Room GetRoom(string roomId)
        {
            try
            {
                Room? room = _context.Rooms.Find(roomId);
                if (room != null)
                {
                    return room;
                }
                throw new ArgumentNullException();
            }
            catch 
            {
                throw;
            }
        }

        public void DeleteRoom(string roomId)
        {
            try
            {
                Room? room = _context.Rooms.Find(roomId);
                if (room != null)
                {
                    _context.Rooms.Remove(room);
                    _context.SaveChanges();
                }
                throw new ArgumentNullException(nameof(room));
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool RoomExists(string roomId)
        {
            return _context.Rooms.Any(e => e.Id == roomId);
        }

        public void UpdateRoom(Room room)
        {
            try
            {
                _context.Entry(room).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
