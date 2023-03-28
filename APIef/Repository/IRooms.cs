using APIef.Models;

namespace APIef.Repository
{
    public interface IRooms
    {
        public void AddRoom(Room room);
        public Room GetRoom(string roomId);
        public void UpdateRoom(Room room);
        public bool RoomExists(string roomId);
        public void DeleteRoom(string roomId);
    }
}
