namespace MovieApitTMDB.Models
{
    public class Room
    {
        public string Id { get; set; }
        public int UsersInRoom { get ; set; }
        public int RoomSize { get; set; } //required 2 to start
        public bool IsStarted { get; set; } = false; //after joining phase ends = true and no one can join
        public bool IsCompleted { get; set; } = false;
    }
}
