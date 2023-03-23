using MovieApitTMDB.Models;

namespace MovieApitTMDB.Services
{
    public class Deserializer
    {
        public List<Movie> DeserializeListOfMovies(dynamic result)
        {
            List<Movie> movies = new List<Movie>();
            foreach (dynamic item in result.results)
            {
                movies.Add(new Movie
                {
                    Title = item.title,
                    Overview = item.overview,
                    PosterPath = item.poster_path,
                    ReleaseDate = item.release_date,
                    BackdropPath = item.backdrop_path,
                    OriginalTitle = item.original_title,
                    VoteAvredge = item.vote_average,
                    Runtime = item.runtime,
                    Popularity = item.popularity,
                    Id = item.id
                });
            }
            return movies;
        }   
           
    }
}
