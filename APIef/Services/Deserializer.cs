using APIef.Models;

namespace APIef.Services
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
                    VoteCount = item.vote_count,
                    Popularity = item.popularity,
                    Id = item.id
                });
            }
            return movies;
        }   
        public Movie DeserializeMovie(dynamic result)
        {
            Movie x = new Movie
            {
                Title = result.title,
                Overview = result.overview,
                PosterPath = result.poster_path,
                ReleaseDate = result.release_date,
                BackdropPath = result.backdrop_path,
                OriginalTitle = result.original_title,
                VoteAvredge = result.vote_average,
                VoteCount = result.vote_count,
                Popularity = result.popularity,
                Id = result.id
            };
            return x;
        }
           
    }
}
