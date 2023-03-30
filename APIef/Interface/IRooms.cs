using APIef.Models;

namespace APIef.Interface
{
    public interface IRooms
    {
        public void AddRoom(Room room);
        public Room GetRoom(string roomId);
        public void UpdateRoom(Room room);
        public bool RoomExists(string roomId);
        public void DeleteRoom(string roomId);
        Task AddRoomAsync(Room room);
        Task<Room> GetRoomAsync(string roomId);
        Task UpdateRoomAsync(Room room);
        Task<bool> RoomExistsAsync(string roomId);
        Task DeleteRoomAsync(string roomId);
    }
}
