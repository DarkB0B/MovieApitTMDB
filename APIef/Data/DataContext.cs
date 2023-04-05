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
        }

    }
}
