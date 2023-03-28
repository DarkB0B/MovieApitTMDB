namespace API.Models
{
    public class Room
    {
        public string Id { get; set; }
        public int UsersInRoom { get ; set; }
        public bool IsStarted { get; set; } = false; 
        public List<List<Movie>> MovieLists { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
