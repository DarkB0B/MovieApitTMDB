using APIef.Models;

namespace APIef.Interface
{
    public interface IRooms
    {       
        Task AddRoomAsync(Room room);
        Task<Room> GetRoomAsync(string roomId);
        Task UpdateRoomAsync(Room room);
        Task<bool> RoomExistsAsync(string roomId);
        Task DeleteRoomAsync(string roomId);
    }
}
