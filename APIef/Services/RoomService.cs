using APIef.Data;
using APIef.Interface;
using APIef.Models;
using Microsoft.EntityFrameworkCore;

namespace APIef.Services
{
    public class RoomService : IRooms
    {
        readonly DataContext _context;
        public RoomService(DataContext context)
        {
            _context = context;
        }

        public async Task AddRoomAsync(Room room)
        {
            try
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<Room> GetRoomAsync(string roomId)
        {
            try
            {
                Room? room = await _context.Rooms.Include(r => r.MovieLists)
                            .ThenInclude(ml => ml.Movies)
                            .FirstOrDefaultAsync(r => r.Id == roomId);
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

        public async Task DeleteRoomAsync(string roomId)
        {
            try
            {
                Room? room = await _context.Rooms.FindAsync(roomId);
                if (room != null)
                {
                    _context.Rooms.Remove(room);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentNullException(nameof(room));
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<bool> RoomExistsAsync(string roomId)
        {
            return await _context.Rooms.AnyAsync(e => e.Id == roomId);
        }

        public async Task UpdateRoomAsync(Room room)
        {
            try
            {
                _context.Entry(room).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
