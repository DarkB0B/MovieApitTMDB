using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace APIef.Models
{
    public class Movie
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string? PosterPath { get; set; }
        public string? ReleaseDate { get; set; }
        public string? BackdropPath { get; set; }
        public string? OriginalTitle { get; set; }
        public string? VoteAvredge { get; set; }
        public string? VoteCount { get; set; }
        public string? Popularity { get; set; }

        [JsonIgnore]
        public ICollection<MovieCollection> MovieCollections { get; set; }
        [JsonIgnore]
        public ICollection<MovieList> MovieLists { get;}
    }
}
