using APIef.Data;
using APIef.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace APItest
{
    [TestClass]
    public class CreateTests
    {

        HttpClient httpClient;
        public string RegularToken;
        public string AdminToken;

        public CreateTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var _httpClient = webAppFactory.CreateDefaultClient();
            httpClient = _httpClient;
            RegularToken = GetToken("regular", "regular").Result;
            AdminToken = GetToken("admin", "admin").Result;
        }
        public async Task<string> GetToken(string username, string pass)
        {


            var userCredentials = new UserCredentials { UserName = username, Password = pass };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Tokens", content);
            var body = await response.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(body);
            JsonElement root = doc.RootElement;
            string token = root.GetProperty("token").GetString();


            return token;
        }


        //--------------User---------------------





        [TestMethod]

        public async Task User1Register()
        {

            var userCredentials = new UserCredentials { UserName = "newuser", Password = "oldpassword" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync("/api/Users", content);

            response.EnsureSuccessStatusCode();

        }


        [TestMethod]
        public async Task LoginWrongPass()
        {


            var userCredentials = new UserCredentials { UserName = "newuser", Password = "xxxx" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Token", content);

            Assert.IsFalse(response.IsSuccessStatusCode);
        }


        [TestMethod]
        public async Task User2Login()
        {

            var userCredentials = new UserCredentials { UserName = "newuser", Password = "oldpassword" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Tokens", content);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body);

        }
       

        [TestMethod]
        public async Task User3ChangePassword()
        {


            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);
            var userCredentials = new ChangePassword { UserName = "newuser", OldPassword = "oldpassword", NewPassword = "newPassword" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"/api/Users/{userCredentials.UserName}", content);


            Assert.IsTrue(response.IsSuccessStatusCode);

        }
        
        [TestMethod]
        public async Task User4IsPasswordChanged()
        {


            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);
            var userCredentials = new UserCredentials { UserName = "newuser", Password = "newPassword" };

            var response = await httpClient.GetAsync($"/api/Users/{userCredentials.UserName}");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(body.Contains("newPassword"));

        }
        [TestMethod]

        public async Task CheckIfUserGetterDosentReturnNull()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);
            var response = await httpClient.GetAsync("/api/Users");
            response.EnsureSuccessStatusCode();
            Assert.IsNotNull(response.Content);
        }


        [TestMethod]

        public async Task User6CheckIfUserGetterGetsnewuser()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);
            var response = await httpClient.GetAsync("/api/Users");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body.Contains("newuser"));
        }

        [TestMethod]
        public async Task DeleteUserDoesNotExist()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);

            var response = await httpClient.DeleteAsync("/api/Users/username");

            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task User6DeleteUser()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);

            var response = await httpClient.DeleteAsync("/api/Users/newuser");

            response.EnsureSuccessStatusCode();
        }


        //---------------Genres-------------------

        [TestMethod]
        public async Task CheckIfGenreGetterHasData()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);

            var response = await httpClient.GetAsync("/api/Genres");

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(body.Contains("Action"));

        }
        [TestMethod]
        public async Task CheckIfGenreGetterDosentReturnNull()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);

            var response = await httpClient.GetAsync("/api/Genres");

            response.EnsureSuccessStatusCode();

            Assert.IsNotNull(response.Content);

        }
        [TestMethod]
        public async Task CanRegularUserCreateGenre()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);

            var genre = new Genre { Name = "TestGenre" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(genre), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Genres", content);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual("Forbidden", response.StatusCode.ToString());

        }

        //-----------------Roles--------------------------

        [TestMethod]
        public async Task CheckIfRoleGetterDosentReturnNull()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);

            var response = await httpClient.GetAsync("/api/Roles");

            response.EnsureSuccessStatusCode();

            Assert.IsNotNull(response.Content);
        }

        [TestMethod]

        public async Task CheckIfRoleGetterHasData()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);
            var response = await httpClient.GetAsync("/api/Roles");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body.Contains("Admin"));
        }




        //----------------MovieCollections------------------

        [TestMethod]

        public async Task PostMovieCollectionRegular()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);

            List<string> movieIds = new List<string> { "19995", "324552", "458156", "505642" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(movieIds), Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync("/api/MovieCollections?title=TestCollection&description=Test", content);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Forbidden);


        }

        [TestMethod]
        public async Task Col1PostMovieCollectionAdmin()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);

            List<string> movieIds = new List<string> { "19995", "324552", "458156", "505642" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(movieIds), Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync("/api/MovieCollections?title=TestCollection&description=Test", content);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);


        }

        [TestMethod]
        public async Task Col2GetMovieCollections()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);

            var response = await httpClient.GetAsync("/api/MovieCollections");
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(body.Contains("TestCollection"));
        }
        [TestMethod]
        public async Task Col3GetMovieCollectionById()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);

            var response = await httpClient.GetAsync("/api/MovieCollections/1");
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body);
            Assert.IsTrue(response.IsSuccessStatusCode);         

        }
        [TestMethod]
        public async Task Col4DeleteMovieCollection()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);

            var response = await httpClient.DeleteAsync("/api/MovieCollections?id=1");
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
        [TestMethod]
        public async Task zzzzCol5GetMovieCollectionByIdAfterDelete()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);

            var response = await httpClient.GetAsync("/api/MovieCollections/1");
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        //--------------Rooms-----------------
        [TestMethod]
        public async Task CreateRoomDiscoverDoSomeStuffAndDelete()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);
            List<int> x = new List<int>();
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(x), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Rooms?option=collection&collectionId=1", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseString);
            string roomId = result.id;

            Debug.WriteLine(roomId);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(responseString.Contains("id"));
            Assert.IsNotNull(roomId);

        }

        [TestMethod]
        public async Task Room2CreateRoomDiscover()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);
            List<int> x = new List<int> { 12, 28};
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(x), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/Rooms?option=discover&movie=true&ammount=10", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseString);
            string roomId = result.id;
            
            Debug.WriteLine(roomId);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(responseString.Contains("id"));
            Assert.IsNotNull(roomId);

            
            //----------Get Room by Id----------------  

                var response2 = await httpClient.GetAsync($"/api/Rooms/{roomId}");
                var responseString2 = await response2.Content.ReadAsStringAsync();
                var result2 = JsonConvert.DeserializeObject<dynamic>(responseString2);

                string thisId = result2.id;
                Debug.WriteLine(thisId);
                Assert.IsTrue(response2.IsSuccessStatusCode);
                Assert.IsTrue(response2.StatusCode == HttpStatusCode.OK);
                Assert.AreEqual(thisId, result2.id.ToString());

            //----------Add User To Room--------------

            var response3 = await httpClient.PatchAsync($"/api/Rooms/{roomId}/users?option=add", null);
            Assert.IsTrue(response3.IsSuccessStatusCode);
            Assert.IsTrue(response3.StatusCode == HttpStatusCode.OK);

            //-----------Start Room----------------
            var response4 = await httpClient.PatchAsync($"/api/Rooms/{roomId}", null);
            var responseString4 = await response4.Content.ReadAsStringAsync();
            var result4 = JsonConvert.DeserializeObject<dynamic>(responseString4);
            Assert.AreEqual(result4.IsStarted.ToString(), "started");
            Assert.AreEqual(result4.users.Count.ToString(), "2");
            Assert.IsTrue(response4.IsSuccessStatusCode);
            Assert.IsTrue(response4.StatusCode == HttpStatusCode.OK);
            //-----------Post MovieList To Room----------------
            List<Movie> movies = new List<Movie> { new Movie { Id = "string", BackdropPath = "string", OriginalTitle = "string", Overview = "string", Popularity = "string", PosterPath = "string", ReleaseDate = "string", Title = "string", VoteAvredge = "string", VoteCount = "string" } };
            var content5 = new StringContent(System.Text.Json.JsonSerializer.Serialize(movies), Encoding.UTF8, "application/json");
            var response5 = await httpClient.PostAsync($"/api/Rooms/{roomId}/movieLists", content5);         
            var response6 = await httpClient.PostAsync($"/api/Rooms/{roomId}/movieLists", content5);

            Assert.IsTrue(response5.IsSuccessStatusCode);
            Assert.IsTrue(response5.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(response6.IsSuccessStatusCode);
            Assert.IsTrue(response6.StatusCode == HttpStatusCode.OK);

            //----------Check If Room Completed
            var response7 = await httpClient.GetAsync($"/api/Rooms/{roomId}");
            var responseString7 = await response2.Content.ReadAsStringAsync();
            var result7 = JsonConvert.DeserializeObject<dynamic>(responseString2);

            bool isCompleted = result7.IsCompleted;
            Debug.WriteLine(isCompleted);
            Assert.IsTrue(response7.IsSuccessStatusCode);
            Assert.IsTrue(response7.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(isCompleted);
            //-----------Delete Room------------
            var response8 = await httpClient.DeleteAsync($"/api/Rooms/{roomId}");
            Assert.IsTrue(response8.IsSuccessStatusCode);
            Assert.IsTrue(response8.StatusCode == HttpStatusCode.OK);


        }
        











    }
    
}


