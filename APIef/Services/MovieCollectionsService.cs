﻿using APIef.Data;
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
                MovieCollection? movieCollection = await _context.MovieCollections.Include(mc => mc.Movies)
            .FirstOrDefaultAsync(mc => mc.Id == id);
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
                return await _context.MovieCollections.Include(mc => mc.Movies).ToListAsync();

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
        public MovieCollection AddMovieListToCollection(MovieCollection movieCollection, List<Movie> movies)
        {
            try
            {
                List<Movie> res = new List<Movie>();
                movies.ForEach(movie =>
                {
                    Movie? moviee =  _context.Movies.Find(movie.Id);
                    if (moviee == null)
                    {
                        res.Add(movie);
                    }
                    else if (moviee != null)
                    {
                        res.Add(moviee);
                    }
                    movieCollection.Movies = res;                
                });
                return movieCollection;
            }
            catch
            {
                throw;
            }
        }


    }
}
