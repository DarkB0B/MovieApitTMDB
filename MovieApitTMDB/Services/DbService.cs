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

        public async void AddGenreToDb(Genre genre)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = genre;
            SetResponse setResponse = client.Set("Genres/" + genre.Id, genre);
        }
    }
}
