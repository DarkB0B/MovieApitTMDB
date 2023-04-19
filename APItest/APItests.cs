using APIef.Data;
using APIef.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
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
            var content = new StringContent(JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Tokens", content);
            var body = await response.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(body);
            JsonElement root = doc.RootElement;
            string token = root.GetProperty("token").GetString();


            return token;
        }


        //--------------User---------------------





        [TestMethod]

        public async Task CreateUser()
        {

            var userCredentials = new UserCredentials { UserName = "newuser", Password = "oldpassword" };
            var content = new StringContent(JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync("/api/Users", content);

            response.EnsureSuccessStatusCode();

        }


        [TestMethod]
        public async Task LoginWrongPass()
        {


            var userCredentials = new UserCredentials { UserName = "newuser", Password = "xxxx" };
            var content = new StringContent(JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Token", content);

            Assert.IsFalse(response.IsSuccessStatusCode);
        }


        [TestMethod]
        public async Task LoginCorrectPass()
        {

            var userCredentials = new UserCredentials { UserName = "newuser", Password = "oldpassword" };
            var content = new StringContent(JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Tokens", content);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body);

        }
        [TestCategory("ChangePassword")]

        [TestMethod]
        public async Task ChangePassword()
        {


            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);
            var userCredentials = new ChangePassword { UserName = "newuser", OldPassword = "oldpassword", NewPassword = "newPassword" };
            var content = new StringContent(JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"/api/Users/{userCredentials.UserName}", content);


            Assert.IsTrue(response.IsSuccessStatusCode);

        }
        [TestCategory("ChangePassword")]
        [TestMethod]
        public async Task IsPasswordChanged()
        {


            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RegularToken);
            var userCredentials = new UserCredentials { UserName = "newuser", Password = "newPassword" };

            var response = await httpClient.GetAsync($"/api/Users/{userCredentials.UserName}");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(body.Contains("newPassword"));

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


        //-----------------Users--------------------------



        [TestMethod]

        public async Task CheckIfUserGetterDosentReturnNull()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);
            var response = await httpClient.GetAsync("/api/Users");
            response.EnsureSuccessStatusCode();
            Assert.IsNotNull(response.Content);
        }


        [TestMethod]

        public async Task CheckIfUserGetterGetsnewuser()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);
            var response = await httpClient.GetAsync("/api/Users");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body.Contains("newuser"));
        }


        //-----------------Deletes------------------------


        [TestMethod]
        public async Task DeleteUserDoesNotExist()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);

            var response = await httpClient.DeleteAsync("/api/Users/username");

            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Deletenewuser()
        {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdminToken);

            var response = await httpClient.DeleteAsync("/api/Users/newuser");

            response.EnsureSuccessStatusCode();
        }

    }
    [TestClass]
    public class CheckTests
    {
        HttpClient httpClient;
        public string RegularToken;
        public string AdminToken;
        public CheckTests()
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
            var content = new StringContent(JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Tokens", content);
            var body = await response.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(body);
            JsonElement root = doc.RootElement;
            string token = root.GetProperty("token").GetString();


            return token;
        }

       

    }
}


