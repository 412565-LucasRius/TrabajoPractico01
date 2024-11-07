﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CineRepository.Models.Entities;

public partial class Screen
{
    public int ScreenId { get; set; }

    public int? CinemaId { get; set; }

    public int? ScreenType { get; set; }

    public int? Capacity { get; set; }

    public int? SeatsTaken { get; set; }

  [JsonIgnore]
    public virtual Cinema Cinema { get; set; }

    public virtual ScreensType ScreenTypeNavigation { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    [JsonIgnore]
    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}