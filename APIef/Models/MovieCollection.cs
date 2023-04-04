using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APIef.Models
{
  
    public class MovieCollection
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } = "https://i.pinimg.com/736x/f2/74/a6/f274a668751e54f62b97a19bc6ce1c2e.jpg";
        public ICollection<Movie> Movies { get; set; }
        //int popularity?
    }
}
