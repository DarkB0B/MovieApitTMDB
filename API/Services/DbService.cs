using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using API.Models;
using Newtonsoft.Json;

namespace API.Services
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

        public async void AddGenreToDb(Genre genre)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = await client.SetAsync("Genres/" + genre.Id, genre);
        }

        public async void AddPremiumUserToDb(string userName)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = new User { UserName = userName, IsPremium = true };
            SetResponse setResponse = await client.SetAsync("Users/" + userName, data);
        }

        //-------Movie

        public async void AddMovieCollectionToDb(MovieCollection movieCollection)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = await client.SetAsync("MovieCollections/" + movieCollection.Id, movieCollection);
        }

        public async Task<MovieCollection> GetMovieCollectionFromDb(int collectionId)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync($"MovieCollections/{collectionId}");
            var result = JsonConvert.DeserializeObject<MovieCollection>(response.Body);
            return result;
        }
        public async Task<List<MovieCollection>> GetAllMovieCollectionsFromDb()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync($"MovieCollections");

            var result = JsonConvert.DeserializeObject<List<MovieCollection>>(response.Body);
            return result;
        }

        //-------ROOM

        public async void AddRoomToDb(Room room)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = await client.SetAsync("Rooms/" + room.Id, room);
        }
        public async Task<Room> GetRoomFromDb(string roomId)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync($"Rooms/{roomId}");
            var result = JsonConvert.DeserializeObject<Room>(response.Body);
            return result;
        }
        public void UpdateRoomInDb(Room room)
        {
            AddRoomToDb(room);
        }

        //-------User
        public async void AddUserToDb(UserCredentials user)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = await client.SetAsync("Users/" + user.UserName, user);
        }
        public async Task<User> GetUserFromDb(string userName)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync($"Users/{userName}");
            var result = JsonConvert.DeserializeObject<User>(response.Body);
            return result;
        }
        public async Task<bool> IsUsernameInDb(string userName)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync($"Users/{userName}");
            if (response.Body == null || response == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<string> AreCredentialsOk(UserCredentials userCredentials)
        {
            client = new FireSharp.FirebaseClient(config);
            string userName = userCredentials.UserName;
            FirebaseResponse response = await client.GetAsync($"Users/{userName}");
            if (response.Body == null || response == null)
            {
                return "wrong username";
            }
            var result = JsonConvert.DeserializeObject<User>(response.Body);
            if (result.Password == userCredentials.Password)
            {
                return "ok";
            }
            return "wrong password";

        }
        public async void UpdatePassword(UserCredentials userCredentials)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync($"Users/{userCredentials.UserName}");
            var result = JsonConvert.DeserializeObject<User>(response.Body);
            result.Password = userCredentials.Password;
            SetResponse setResponse = await client.SetAsync("Users/" + result.UserName, result);
        }
        // ------- Roles
        public async void AddRoleToDb(Role role)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse setResponse = await client.SetAsync("Roles/" + role.Name, role);
        }
        public async Task<Role> GetRoleFromDb(string roleName)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync($"Roles/{roleName}");
            var result = JsonConvert.DeserializeObject<Role>(response.Body);
            return result;
        }
        public async Task<List<Role>> GetAllRolesFromDb()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync($"Roles");
            var result = JsonConvert.DeserializeObject<List<Role>>(response.Body);
            return result;
        }
    }
}
