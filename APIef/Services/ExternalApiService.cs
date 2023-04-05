using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using Newtonsoft.Json;
using APIef.Data;

namespace APIef.Services
{
    public class ExternalApiService
    {
        private string apiKey = "2c3773c58b96fc195869c5f3162ff399";
        private readonly DataContext _context;
        Deserializer deserializer = new Deserializer();
        public ExternalApiService(DataContext context)
        {
            _context = context;
        }


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
                        dbId = 0,
                        tmdbId = item.id,
                        Name = item.name
                    }); 
                }
                return genres;
            }
        }
        public async Task<List<Movie>> GetMoviesPerGenre(int genreId, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3//discover/movie?api_key={apiKey}&language=en-US\r\n&with_genres={genreId}&page={page}&sort_by=popularity.desc&vote_average.gte=7.5"),
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

        public async Task<List<Movie>> GetMoviesById(List<string> movieIds)
        {
            List<Movie> movies = new List<Movie>();
            for (int i = 0; i < movieIds.Count; i++)
            {
                if (movieIds[i] == null)
                {
                    continue;
                }
                Movie? moviedb = await _context.Movies.FindAsync(movieIds[i]);
                if (moviedb == null)
                {

                    string id = movieIds[i];
                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"\r\nhttps://api.themoviedb.org/3/movie/{id}?api_key={apiKey}&language=en-US"),
                    };

                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(body);
                        if (result == null)
                        {
                            throw new ArgumentNullException("API returned null");
                        }
                        Movie? movie = deserializer.DeserializeMovie(result);
                        if (movie != null)
                        {
                            movies.Add(movie);
                        }
                    }
                }
                else
                {
                    movies.Add(moviedb);
                }
            }
            return movies;

        }

    }
}
