using System.ComponentModel.DataAnnotations;

namespace APIef.Models
{
    public class MovieList
    {
        [Key]
        public string Id { get; set; } 
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
