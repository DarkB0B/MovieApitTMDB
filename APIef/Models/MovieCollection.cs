using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APIef.Models
{
    [Keyless]
    public class MovieCollection
    {
        
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Movie> Movies { get; set; }
        //int popularity?
    }
}
