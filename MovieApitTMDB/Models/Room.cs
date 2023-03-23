namespace MovieApitTMDB.Models
{
    public class Room
    {
        public string Id { get; set; }
        public List<string> UserIdList { get; set; } = new List<string>();
        public int RoomSize { get; set; } //required 2 to start
        public bool IsStarted { get; set; } = false; //after joining phase ends = true and no one can join
    }
}
