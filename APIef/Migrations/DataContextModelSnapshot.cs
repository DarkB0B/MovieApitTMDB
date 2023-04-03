﻿// <auto-generated />
using System;
using APIef.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIef.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIef.Models.Genre", b =>
                {
                    b.Property<int>("dbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("dbId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tmdbId")
                        .HasColumnType("int");

                    b.HasKey("dbId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("APIef.Models.Movie", b =>
                {
                    b.Property<int>("dbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("dbId"));

                    b.Property<string>("BackdropPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int?>("MovieCollectionId")
                        .HasColumnType("int");

                    b.Property<int?>("MovieListId")
                        .HasColumnType("int");

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Popularity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TmdbId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoteAvredge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoteCount")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("dbId");

                    b.HasIndex("MovieCollectionId");

                    b.HasIndex("MovieListId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("APIef.Models.MovieCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovieCollections");
                });

            modelBuilder.Entity("APIef.Models.MovieList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoomId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("MovieLists");
                });

            modelBuilder.Entity("APIef.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("APIef.Models.Room", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("bit");

                    b.Property<int>("UsersInRoom")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("APIef.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsPremium")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserName");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("APIef.Models.Movie", b =>
                {
                    b.HasOne("APIef.Models.MovieCollection", null)
                        .WithMany("Movies")
                        .HasForeignKey("MovieCollectionId");

                    b.HasOne("APIef.Models.MovieList", null)
                        .WithMany("Movies")
                        .HasForeignKey("MovieListId");
                });

            modelBuilder.Entity("APIef.Models.MovieList", b =>
                {
                    b.HasOne("APIef.Models.Room", null)
                        .WithMany("MovieLists")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("APIef.Models.User", b =>
                {
                    b.HasOne("APIef.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("APIef.Models.MovieCollection", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("APIef.Models.MovieList", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("APIef.Models.Room", b =>
                {
                    b.Navigation("MovieLists");
                });
#pragma warning restore 612, 618
        }
    }
}
