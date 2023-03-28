using System.ComponentModel.DataAnnotations;

namespace APIef.Models
{
    public class Room
    {
        [Key]
        public string Id { get; set; }
        public int UsersInRoom { get ; set; }
        public bool IsStarted { get; set; } = false; 
        public List<List<Movie>> MovieLists { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
