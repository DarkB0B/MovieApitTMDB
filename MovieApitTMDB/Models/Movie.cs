namespace MovieApitTMDB.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; } = string.Empty;
        public string ReleaseDate { get; set; }
        public string? BackdropPath { get; set; }
        public string? OriginalTitle { get; set; }
        public List<string>? GenreIds { get; set; }
        public string? Popularity { get; set; }
        public int Id { get; set; }

        
    }
}
