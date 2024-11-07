﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CineRepository.Models.Entities;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? ProducerId { get; set; }

    public int? GenreId { get; set; }

    public int? ClasificationId { get; set; }

    public int? Duration { get; set; }

    public virtual Clasification Clasification { get; set; }

    public virtual Genre Genre { get; set; }

    public virtual ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();

    public virtual ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();

    public virtual Producer Producer { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

  [JsonIgnore]
    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();

    public virtual ICollection<UserMovieHistory> UserMovieHistories { get; set; } = new List<UserMovieHistory>();
}