﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class Clasification
{
    public int ClasificationId { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}