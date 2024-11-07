﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CineRepository.Models.Entities;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    [JsonIgnore]
    public virtual ICollection<UserGenreStat> UserGenreStats { get; set; } = new List<UserGenreStat>();
}