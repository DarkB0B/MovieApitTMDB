using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using MovieApitTMDB.Models;

namespace MovieApitTMDB.Services
{
    public class DbService
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "BpCO4ueDkrgbvbsW7Qgsm6HOMyLgwqXVmQYB0QfP\r\n",
            BasePath = "https://shrex-fa4af-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client;

        public void AddGenreToDb(Genre genre)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = client.Set("Genres/" + genre.Id, genre);
        }

        public void AddPremiumUserToDb(string userId)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = new User { Id = userId, IsPremium = true };
            SetResponse setResponse = client.Set("Users/" + userId, data);
        }

        public void AddMovieCollectionToDb(MovieCollection movieCollection)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = client.Set("MovieCollections/" +  movieCollection.Id, movieCollection);
        }
    }
}
