using System.ComponentModel.DataAnnotations;

namespace APIef.Models
{
    public class Genre
    {
        [Key]
        public int dbId { get; set; } = 0;
        public int tmdbId { get; set; }
        public string Name { get; set; }
    }
}
