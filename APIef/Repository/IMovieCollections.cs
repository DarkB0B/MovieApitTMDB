using APIef.Models;

namespace APIef.Repository
{
    public interface IMovieCollections
    {
        public void AddMovieCollection(MovieCollection movieCollection);
        public MovieCollection GetMovieCollection(int id);
        public void UpdateMovieCollection(MovieCollection movieCollection);
        public List<MovieCollection> GetMoviesCollections();
        public bool MovieCollectionExists(int id);
        public void DeleteMovieCollection(int id);
    }
}
