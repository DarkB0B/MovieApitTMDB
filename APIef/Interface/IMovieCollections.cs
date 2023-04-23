using APIef.Models;

namespace APIef.Interface
{
    public interface IMovieCollections
    {
        Task AddMovieCollectionAsync(MovieCollection movieCollection);
        Task<MovieCollection> GetMovieCollectionAsync(int id);
        Task UpdateMovieCollectionAsync(MovieCollection movieCollection);
        Task<List<MovieCollection>> GetMovieCollectionsAsync();
        Task<bool> MovieCollectionExistsAsync(int id);
        Task DeleteMovieCollectionAsync(int id);
        public MovieCollection AddMovieListToCollection(MovieCollection movieCollection, List<Movie> movies);

    }
}
