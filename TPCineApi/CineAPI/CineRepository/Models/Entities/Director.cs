﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class Director
{
    public int DirectorId { get; set; }

    public string Name { get; set; }

    public DateTime? BirthDate { get; set; }

    public virtual ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
}