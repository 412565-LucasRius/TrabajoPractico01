﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CineRepository.Models.Entities;

public partial class Cinema
{
    public int CinemaId { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    [JsonIgnore]
    public virtual ICollection<Screen> Screens { get; set; } = new List<Screen>();
}