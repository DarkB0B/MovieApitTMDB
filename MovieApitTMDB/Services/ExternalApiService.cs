using FireSharp.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using MovieApitTMDB.Models;
using Newtonsoft.Json;

namespace MovieApitTMDB.Services
{
    public class ExternalApiService
    {
        private string apiKey = "2c3773c58b96fc195869c5f3162ff399";

        Deserializer deserializer = new Deserializer();


        public async Task<List<Genre>> GetGenres()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3/genre/movie/list?api_key={apiKey}&language=en-US\r\n"),

            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(body);

                List<Genre> genres = new List<Genre>();
                foreach (dynamic item in result.genres)
                {
                    genres.Add(new Genre
                    {
                        Id = item.id,
                        Name = item.name
                    });
                }
                return genres;
            }
        }
        public async Task<List<Movie>> GetMoviesPerGenre(int genreId)
        {
            int page = 1;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3//discover/movie?api_key={apiKey}&language=en-US\r\n&with_genres={genreId}&page={page}&sort_by=vote_average.desc"),
            };

            using (var response = await client.SendAsync(request))
            {              
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(body);
                if(result == null)
                {
                    throw new ArgumentNullException("API returned null");
                }
                List<Movie> movies = deserializer.DeserializeListOfMovies(result);
                return movies;
            }

        }

    }
}
