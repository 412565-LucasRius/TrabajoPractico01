﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class Seat
{
    public int SeatId { get; set; }

    public int? ScreenId { get; set; }

    public int? SeatNumber { get; set; }

    public string SeatRow { get; set; }

    public string SeatStatus { get; set; }

    public virtual Screen Screen { get; set; }
}