﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class MovieCast
{
    public int MovieCastId { get; set; }

    public int? MovieId { get; set; }

    public int? ActorId { get; set; }

    public string Role { get; set; }

    public virtual Actor Actor { get; set; }

    public virtual Movie Movie { get; set; }
}