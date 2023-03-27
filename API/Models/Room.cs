namespace API.Models
{
    public class Room
    {
        public string Id { get; set; }
        public int UsersInRoom { get ; set; }
        public bool IsStarted { get; set; } = false; //after joining phase ends = true and no one can join
        public List<List<Movie>> MovieLists { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
