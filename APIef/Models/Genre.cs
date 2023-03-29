using System.ComponentModel.DataAnnotations;

namespace APIef.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
