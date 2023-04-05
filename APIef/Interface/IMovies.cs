using APIef.Models;

namespace APIef.Interface
{
    public interface IMovies
    {
        Task AddMovieAsync(Movie movie);
        Task<Movie> GetMovieAsync(int movieId);
        Task<bool> MovieExistsAsync(int movieId);
        Task<List<Movie>> GetMoviesAsync();
    }
}
