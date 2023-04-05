using APIef.Data;
using APIef.Interface;
using APIef.Models;
using Microsoft.EntityFrameworkCore;

namespace APIef.Services
{
    public class MoviesService : IMovies
    {
        private readonly DataContext _context;
        public MoviesService(DataContext context)
        {
            _context = context;
        }
        public async Task AddMovieAsync(Movie movie)
        {
            try
            {
                await _context.Movies.AddAsync(movie);
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
            
        }

        public async Task<Movie> GetMovieAsync(int movieId)
        {
            try
            {
                Movie? movie = await _context.Movies.FindAsync(movieId);
                if (movie == null)
                {
                    throw new ArgumentNullException();
                }     
                return movie;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
                List<Movie> movies = await _context.Movies.ToListAsync();
                return movies;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        public async Task<bool> MovieExistsAsync(int movieId)
        {
            try
            {
                Movie? movie = await _context.Movies.FindAsync(movieId);
                if (movie == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }
    
    }
}
