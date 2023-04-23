using Microsoft.EntityFrameworkCore;
using APIef.Models;

namespace APIef.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieCollection> MovieCollections { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<MovieList> MovieLists { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCollection>()
                .HasMany(x => x.Movies)
                .WithMany(y => y.MovieCollections)
                .UsingEntity(z => z.ToTable("MovieCollectionMovies"));

            modelBuilder.Entity<MovieList>()
                .HasMany(x => x.Movies)
                .WithMany(y => y.MovieLists)
                .UsingEntity(z => z.ToTable("MovieMovieLists"));

            modelBuilder.Entity<Role>().HasData(
               new Role { Name = "Regular", RoleId = 1 },
               new Role { Name = "Premium", RoleId = 2 },
               new Role { Name = "Admin", RoleId = 3 }
           );
            modelBuilder.Entity<User>().HasData(
    new User
    {
        UserName = "admin",
        Password = "admin",
        IsPremium = true,
        RoleId = 3
    },
    new User
    {
        UserName = "regular",
        Password = "regular",
        IsPremium = false,
        RoleId = 1
    }
);
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Name = "Action", tmdbId = 28, dbId = 1 },
                new Genre { Name = "Adventure", tmdbId = 12, dbId = 2 },
                new Genre { Name = "Animation", tmdbId = 16, dbId = 3 },
                new Genre { Name = "Comedy", tmdbId = 35, dbId = 4 },
                new Genre { Name = "Crime", tmdbId = 80, dbId = 5 },
                new Genre { Name = "Documentary", tmdbId = 99, dbId = 6 },
                new Genre { Name = "Drama", tmdbId = 18, dbId = 7 },
                new Genre { Name = "Family", tmdbId = 10751, dbId = 8 },
                new Genre { Name = "Fantasy", tmdbId = 14, dbId = 9 },
                new Genre { Name = "History", tmdbId = 36, dbId = 10 },
                new Genre { Name = "Horror", tmdbId = 27, dbId = 11 },
                new Genre { Name = "Music", tmdbId = 10402, dbId = 12 },
                new Genre { Name = "Mystery", tmdbId = 9648, dbId = 13 },
                new Genre { Name = "Romance", tmdbId = 10749, dbId = 14 },
                new Genre { Name = "Science Fiction", tmdbId = 878, dbId = 15 },
                new Genre { Name = "TV Movie", tmdbId = 10770, dbId = 16 },
                new Genre { Name = "Thriller", tmdbId = 53, dbId = 17 },
                new Genre { Name = "War", tmdbId = 10752, dbId = 18 },
                new Genre { Name = "Western", tmdbId = 37, dbId = 19 }
                );

            
            

        }
        


    }
}
