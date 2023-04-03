﻿using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace APIef.Models
{
    public class Movie
    {
        [Key]
        public int dbId { get; set; } = 0;
        public string TmdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string? PosterPath { get; set; }
        public string? ReleaseDate { get; set; }
        public string? BackdropPath { get; set; }
        public string? OriginalTitle { get; set; }
        public string? VoteAvredge { get; set; }
        public string? VoteCount { get; set; }
        public string? Popularity { get; set; }
        public int Likes { get; set; } = 0;
    }
}
