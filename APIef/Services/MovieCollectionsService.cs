using APIef.Data;
using APIef.Interface;
using APIef.Models;
using Microsoft.EntityFrameworkCore;

namespace APIef.Services
{
    public class MovieCollectionsService : IMovieCollections
    {
        private readonly DataContext _context;
        public MovieCollectionsService(DataContext context)
        {
            _context = context;
        }
        //implement IMovieCollections

        public void AddMovieCollection(MovieCollection movieCollection)
        {
            try
            {
                _context.MovieCollections.Add(movieCollection);
                _context.SaveChanges();
            }
            catch
            {
                throw;

            }

        }

        public void DeleteMovieCollection(int id)
        {
            try
            {
                MovieCollection? movieCollection = _context.MovieCollections.Find(id);
                if (movieCollection != null)
                {
                    _context.MovieCollections.Remove(movieCollection);
                    _context.SaveChanges();
                }
                throw new ArgumentNullException(nameof(movieCollection));
            }
            catch
            {
                throw;
            }

        }

        public MovieCollection GetMovieCollection(int id)
        {
            try
            {
                MovieCollection? movieCollection = _context.MovieCollections.Find(id);
                if (movieCollection != null)
                {
                    return movieCollection;
                }
                else
                {
                    throw new ArgumentNullException(nameof(movieCollection));
                }
            }
            catch
            {
                throw;
            }
        }

        public List<MovieCollection> GetMovieCollections()
        {
            try
            {
                return _context.MovieCollections.ToList();
            }
            catch
            {
                throw;
            }
        }

        public bool MovieCollectionExists(int id)
        {
            return _context.MovieCollections.Any(e => e.Id == id);
        }

        public void UpdateMovieCollection(MovieCollection movieCollection)
        {
            try
            {
                _context.Entry(movieCollection).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public async Task AddMovieCollectionAsync(MovieCollection movieCollection)
        {
            try
            {
                _context.MovieCollections.Add(movieCollection);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }

        public async Task DeleteMovieCollectionAsync(int id)
        {
            try
            {
                MovieCollection movieCollection = await _context.MovieCollections.FindAsync(id);
                if (movieCollection != null)
                {
                    _context.MovieCollections.Remove(movieCollection);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentNullException(nameof(movieCollection));
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<MovieCollection> GetMovieCollectionAsync(int id)
        {
            try
            {
                MovieCollection movieCollection = await _context.MovieCollections.FindAsync(id);
                if (movieCollection != null)
                {
                    return movieCollection;
                }
                else
                {
                    throw new ArgumentNullException(nameof(movieCollection));
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<MovieCollection>> GetMovieCollectionsAsync()
        {
            try
            {
                return await _context.MovieCollections.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> MovieCollectionExistsAsync(int id)
        {
            return await _context.MovieCollections.AnyAsync(e => e.Id == id);
        }

        public async Task UpdateMovieCollectionAsync(MovieCollection movieCollection)
        {
            try
            {
                _context.Entry(movieCollection).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }


    }
}
