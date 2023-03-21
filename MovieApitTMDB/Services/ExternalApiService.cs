using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using MovieApitTMDB.Models;
using Newtonsoft.Json;

namespace MovieApitTMDB.Services
{
    public class ExternalApiService
    {
        string apiKey = "2c3773c58b96fc195869c5f3162ff399";

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
        public async Task<List<Movie>> GetMoviesPerGenre(List<int> genreIds)
        {
            int genreId = genreIds[0];
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3//discover/movie?with_genres={genreId}&sort_by=vote_average.desc?api_key={apiKey}&language=en-US\r\n"),
            };
            
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(body);
                //List<Movie> movies = new List<Movie>();


                return result;
            }



                
        }

    }
}
