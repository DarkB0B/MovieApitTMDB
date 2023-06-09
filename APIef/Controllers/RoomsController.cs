﻿
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Data;
using APIef.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace APIef.Controllers
{
    [Authorize(Roles = "Regular, Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRooms _roomService;
        private readonly DataContext _context;
        private readonly IMovies _movieService;
        private readonly IMovieCollections _movieCollectionsService;
        IConfiguration _configuration;
        readonly ExternalApiService externalApiService;
        public RoomsController(IRooms roomService, DataContext context, IMovies movieService, IMovieCollections movieCollectionsService, IConfiguration configuration)
        {
            _configuration = configuration;
           string apiKey = _configuration.GetValue<string>("ApiKey");
            externalApiService = new ExternalApiService(apiKey);
            _movieCollectionsService = movieCollectionsService;
            _movieService = movieService;
            _roomService = roomService;
            _context = context;
        }
        //get room by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                Room room = await _roomService.GetRoomAsync(id);
                Console.WriteLine("Room Sent");
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //edit room
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Room room)
        {
            try
            {
                if (id != room.Id)
                {
                    return BadRequest("Room id does not match request id");
                }

                await _roomService.UpdateRoomAsync(room);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
        //This method starts room when there are at least 2 people connected
        [HttpPatch("{id}")]
        public async Task<IActionResult> StartRoom(string id)
        {
            try
            {
                Room room = await _roomService.GetRoomAsync(id);
                if (room.UsersInRoom >= 2)
                {
                    room.IsStarted = true;
                    await _roomService.UpdateRoomAsync(room);
                    Console.WriteLine("Room Started");
                    return Ok(room);
                }
                Console.WriteLine("Not Enough People Joined The Room");
                return BadRequest ("Not Enough People Joined The Room");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //create new room
        [HttpPost]
        public async Task<IActionResult> Post(String option, List<int>? genreList, bool? movie, int? ammount, int? collectionId)
        {

            int maxAmmount = 50;
            int minAmmount = 10;
            //filtering out invalid options
            if (option == null || (option != "collection" && option != "discover"))
            {
                return BadRequest();
            }
            Room room = new Room
            {
                Id = CodeGenerator.RandomString(5),
                UsersInRoom = 1,
                IsStarted = false,
                IsCompleted = false,
            };
            try
            {
                while(await _roomService.RoomExistsAsync(room.Id) == true)
                {
                    room.Id = CodeGenerator.RandomString(5);
                }
             
                await _roomService.AddRoomAsync(room);
                if(option == "collection") 
                {
                    if(collectionId == null)
                    {
                        return BadRequest();
                    }

                    MovieCollection? movieCollection = await _movieCollectionsService.GetMovieCollectionAsync((int)collectionId);

                    if(movieCollection == null)
                    {
                        return BadRequest("Wrong CollectionId");
                    }

                    MovieList starter = new MovieList { Movies = movieCollection.Movies, Id = room.Id + "starter" };
                    await _context.MovieLists.AddAsync(starter);
                    await _context.SaveChangesAsync();
                    await _roomService.AddListToRoomAsync(room.Id, starter);
                    return Ok(room);
                }
                else if(option == "discover")
                {
                    if (movie == null || ammount == null || (ammount > maxAmmount && ammount < minAmmount) || genreList == null || genreList.Count > 3 || genreList.Count < 1)
                    {
                        return BadRequest("Missing required fields");
                    }
                    
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
                    
                    List<Movie> res = _roomService.ValidateMovies(result);
                    foreach(Movie x in res)
                    {
                        Console.WriteLine(x.Id);
                    }
                    MovieList starter = new MovieList { Movies = res, Id = room.Id + "starter" };
                    await _context.MovieLists.AddAsync(starter);
                    await _context.SaveChangesAsync();
                    await _roomService.AddListToRoomAsync(room.Id, starter);
                    return Ok(room);
                }

                return BadRequest();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/users")]
        public async Task<IActionResult> AddUserToRoom(string id, string option)
        {
            try
            {
                if (option == "add")
                {
                    Room room = await _roomService.GetRoomAsync(id);
                    room.UsersInRoom++;
                    await _roomService.UpdateRoomAsync(room);
                    return Ok();
                }
                else if (option == "remove")
                {
                    Room room = await _roomService.GetRoomAsync(id);
                    room.UsersInRoom--;
                    await _roomService.UpdateRoomAsync(room);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        //This method adds movie list to room and if all users have added their movie list, it sets the room to completed and generates final movie list
        [HttpPost("{id}/movielists")]
        public async Task<IActionResult> AddMovieListToRoom(string id, [FromBody] List<Movie> movies) //add movie list to room
        {  
            try
            {
                Console.WriteLine("Got the list");
                Room room = await _roomService.GetRoomAsync(id);
                string listId = room.Id + room.MovieLists.Count.ToString();
                List<Movie> movieList = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    Movie? x = await _movieService.GetMovieAsync(movie.Id);
                    if(x == null)
                    {
                        movieList.Add(movie);
                    }
                    else if(x != null)
                    {
                        movieList.Add(x);
                    }
                }
                MovieList list = new MovieList { Movies = movieList, Id = listId };
                await _context.MovieLists.AddAsync(list);
                await _context.SaveChangesAsync();
                
                if (room.IsStarted && !room.IsCompleted) 
                {
                    await _roomService.AddListToRoomAsync(id , list);

                    if (room.MovieLists.Count == room.UsersInRoom + 1)
                    {
                        room.IsCompleted = true;
                        MovieList finalList = _roomService.GetFinalList(room);
                        await _context.MovieLists.AddAsync(finalList);
                        await _roomService.UpdateRoomAsync(room);
                        await _context.SaveChangesAsync();
                        await _roomService.AddListToRoomAsync(id, finalList);
                        await _context.SaveChangesAsync();
                        return Ok("Picking Phase Completed");
                    }

                    await _roomService.UpdateRoomAsync(room);
                    return Ok("MovieList Updated");
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        //delete room and asociated lists
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRoom(string id)
        {
            Room room = await _roomService.GetRoomAsync(id);
            foreach(MovieList list in room.MovieLists)
            {
                _context.MovieLists.Remove(list);
            }
            await _context.SaveChangesAsync();
            await _roomService.DeleteRoomAsync(id);
            return Ok();
        }
    }
}