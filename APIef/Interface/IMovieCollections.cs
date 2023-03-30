using APIef.Models;

namespace APIef.Interface
{
    public interface IMovieCollections
    {
        public void AddMovieCollection(MovieCollection movieCollection);
        public MovieCollection GetMovieCollection(int id);
        public void UpdateMovieCollection(MovieCollection movieCollection);
        public List<MovieCollection> GetMovieCollections();
        public bool MovieCollectionExists(int id);
        public void DeleteMovieCollection(int id);
        Task AddMovieCollectionAsync(MovieCollection movieCollection);
        Task<MovieCollection> GetMovieCollectionAsync(int id);
        Task UpdateMovieCollectionAsync(MovieCollection movieCollection);
        Task<List<MovieCollection>> GetMovieCollectionsAsync();
        Task<bool> MovieCollectionExistsAsync(int id);
        Task DeleteMovieCollectionAsync(int id);
    }
}
