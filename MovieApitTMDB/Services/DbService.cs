using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using MovieApitTMDB.Models;
using Newtonsoft.Json;

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

        //------Genre

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

        //-------Movie

        public void AddMovieCollectionToDb(MovieCollection movieCollection)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = client.Set("MovieCollections/" +  movieCollection.Id, movieCollection);
        }  

        public MovieCollection GetMovieCollectionFromDb(int collectionId) 
        {           
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse response = client.Get($"MovieCollections/{collectionId}");
                var result = JsonConvert.DeserializeObject<MovieCollection>(response.Body);
                return result;           
        }
        public List<MovieCollection> GetAllMovieCollectionsFromDb()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get($"MovieCollections");

            var result = JsonConvert.DeserializeObject<List<MovieCollection>>(response.Body);
            return result;
        }

        //-------ROOM

        public void AddRoomToDb(Room room)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = client.Set("Rooms/" + room.Id, room);
        }
        public Room GetRoomFromDb(string roomId)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get($"Rooms/{roomId}");
            var result = JsonConvert.DeserializeObject<Room>(response.Body);
            return result;
        }
        public void UpdateRoomInDb(Room room)
        {
            AddRoomToDb(room);           
        }

    }
}
