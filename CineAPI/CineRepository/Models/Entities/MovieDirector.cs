﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class MovieDirector
{
    public int MovieDirectorId { get; set; }

    public int? MovieId { get; set; }

    public int? DirectorId { get; set; }

    public virtual Director Director { get; set; }

    public virtual Movie Movie { get; set; }
}