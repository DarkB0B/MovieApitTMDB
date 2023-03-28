using APIef.Data;
using APIef.Interface;
using APIef.Models;
using Microsoft.EntityFrameworkCore;

namespace APIef.Repository
{
    public class MovieCollectionRepository : IMovieCollections
    {
        private readonly DataContext _context;
        public MovieCollectionRepository(DataContext context)
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

       
    }
}
