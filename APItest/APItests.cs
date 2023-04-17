using APIef.Data;
using APIef.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace APItest
{
    [TestClass]
    public class UnitTest1
    {

        public string Token;

        //-----------------Users--------------------------
        [TestMethod]

        public async Task CheckIfUserGetterDosentReturnNull()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var response = await httpClient.GetAsync("/api/Users");
            response.EnsureSuccessStatusCode();
            Assert.IsNotNull(response.Content);
        }

        
        [TestMethod]

        public async Task CheckIfUserGetterGetsnewuser()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var response = await httpClient.GetAsync("/api/Users");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body.Contains("newuser"));
        }

        [TestMethod]

        public async Task CheckIfUserPasswordChanged()
        {

            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var response = await httpClient.GetAsync("/api/Users");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body.Contains("changedpass123"));
        }
        [TestMethod]
        public async Task Login()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var userCredentials = new UserCredentials { UserName = "newuser", Password = "newpassword" };
            var content = new StringContent(JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/Tokens", content);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body);
            Console.WriteLine(body);
        }


        //---------------Genres-------------------

        [TestMethod]
        public async Task CheckIfGenreGetterHasData()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("/api/Genres");

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(body.Contains("Action"));

        }
        [TestMethod]
        public async Task CheckIfGenreGetterDosentReturnNull()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("/api/Genres");

            response.EnsureSuccessStatusCode();

            Assert.IsNotNull(response.Content);

        }

        //-----------------Roles--------------------------

        [TestMethod]
        public async Task CheckIfRoleGetterDosentReturnNull()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("/api/Genres");

            response.EnsureSuccessStatusCode();

            Assert.IsNotNull(response.Content);
        }

        [TestMethod]

        public async Task CheckIfRoleGetterHasData()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var response = await httpClient.GetAsync("/api/Roles");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(body.Contains("Admin"));
        }


        

        
        //------------------Delete-----------
        

        [TestMethod]
        public async Task DeleteUserDoesNotExist()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.DeleteAsync("/api/Users/username");

           Assert.IsFalse(response.IsSuccessStatusCode);
        }
        
        [TestMethod]
        public async Task Deletenewuser()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.DeleteAsync("/api/Users/newuser");

            response.EnsureSuccessStatusCode();
        }
        

        



    }

}