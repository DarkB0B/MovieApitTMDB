using System.ComponentModel.DataAnnotations;

namespace APIef.Models
{
    public class MovieList
    {
        [Key]
        public int Id { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
