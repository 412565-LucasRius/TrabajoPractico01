﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class UserGenreStat
{
    public int UserAccountId { get; set; }

    public int GenreId { get; set; }

    public int? ViewCount { get; set; }

    public virtual Genre Genre { get; set; }

    public virtual UserAccount UserAccount { get; set; }
}