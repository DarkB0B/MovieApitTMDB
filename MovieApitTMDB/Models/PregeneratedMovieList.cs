namespace MovieApitTMDB.Models
{
    public class PregeneratedMovieList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
