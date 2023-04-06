using APIef.Models;

namespace APIef.Interface
{
    public interface IMovies
    {
        Task AddMovieAsync(Movie movie);
        Task<Movie> GetMovieAsync(string movieId);
        Task<bool> MovieExistsAsync(string movieId);
        Task<List<Movie>> GetMoviesAsync();
    }
}
