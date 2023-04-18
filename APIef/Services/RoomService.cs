using APIef.Data;
using APIef.Interface;
using APIef.Models;
using Microsoft.EntityFrameworkCore;

namespace APIef.Services
{
    public class RoomService : IRooms
    {
        readonly DataContext _context;

        public RoomService(DataContext context)
        {
            _context = context;
        }

        public async Task AddRoomAsync(Room room)
        {
            try
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<Room> GetRoomAsync(string roomId)
        {
            try
            {
                Room? room = await _context.Rooms.Include(r => r.MovieLists)
                            .ThenInclude(ml => ml.Movies)
                            .FirstOrDefaultAsync(r => r.Id == roomId);
                if (room != null)
                {
                    return room;
                }
                throw new ArgumentNullException();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteRoomAsync(string roomId)
        {
            try
            {
                Room? room = await _context.Rooms.FindAsync(roomId);
                if (room != null)
                {
                    _context.Rooms.Remove(room);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentNullException(nameof(room));
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<bool> RoomExistsAsync(string roomId)
        {
            return await _context.Rooms.AnyAsync(e => e.Id == roomId);
        }

        public async Task UpdateRoomAsync(Room room)
        {
            try
            {
                _context.Entry(room).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task AddListToRoomAsync(string roomId, MovieList movieList)
        {
            try
            {
                Room? room = await _context.Rooms.FindAsync(roomId);
                if (room != null)
                {
                    room.MovieLists.Add(movieList);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentNullException(nameof(room));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<Movie> ValidateMovies(List<Movie> movies)
        {
            try
            {
                List<Movie> res = new List<Movie>();
                movies.ForEach(movie =>
                {
                    Movie? moviee = _context.Movies.Find(movie.Id);
                    if (moviee == null)
                    {
                        res.Add(movie);
                    }
                    else if (moviee != null)
                    {
                        res.Add(moviee);
                    }
                    
                });
                return res;
            }
            catch
            {
                throw;
            }
        }




        public MovieList GetFinalList( Room room)
        {
            double threshold = room.MovieLists.Count * 0.7;

            Dictionary<Movie, int> objCount = new Dictionary<Movie, int>();

            foreach (MovieList thislist in room.MovieLists)
            {
                foreach (Movie obj in thislist.Movies)
                {
                    if (objCount.ContainsKey(obj))
                    {
                        objCount[obj]++;
                    }
                    else
                    {
                        objCount.Add(obj, 1);
                    }
                }
            }

            List<Movie> commonMovies = new List<Movie>();
            foreach (KeyValuePair<Movie, int> entry in objCount)
            {
                if (entry.Value >= threshold)
                {
                    commonMovies.Add(entry.Key);
                }
            }

            MovieList finalList = new MovieList { Movies = commonMovies, Id = room.Id + "final" };
            return finalList;
        }
        /*
        public async Task<List<Movie>> GenerateStarterListDiscover(List<int> genreList, bool movie, int ammount)
        {
            List<Movie> result = new List<Movie>();

            int moviesPerGenre = (int)ammount / genreList.Count;



            Random rnd = new Random();
            foreach (int genreId in genreList)
            {
                int moviesAdded = 0;
                int page = 1;
                while (true)
                {
                    Console.WriteLine("Page: " + page + " Genre Id: " + genreId);
                    //TODO: make loop flow if there arent enough movies in one page from api
                    List<Movie> moviesForGenre = await externalApiService.GetMoviesPerGenre(genreId, page, movie);
                    for (int i = 0; i < 5; i++)
                    {
                        int index = rnd.Next(0, moviesForGenre.Count);
                        moviesForGenre.RemoveRange(index, 1);
                    }
                    // Add up to `moviesPerGenre` movies from the current genre to the result list

                    foreach (Movie thismovie in moviesForGenre)
                    {

                        if (result.Find(x => x.Id == thismovie.Id) == null)
                        {
                            result.Add(thismovie);
                            moviesAdded++;
                        }

                        if (moviesAdded >= moviesPerGenre)
                        {
                            break;
                        }

                    }

                    // If we still don't have enough movies, page ++
                    if (moviesAdded <= moviesPerGenre)
                    {
                        page++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return result;
        }
        */
    }
}
