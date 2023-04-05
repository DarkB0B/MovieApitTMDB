using System.ComponentModel.DataAnnotations;

namespace APIef.Models
{
    public class MovieList
    {
        [Key]
        public int Id { get; set; } = 0;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
