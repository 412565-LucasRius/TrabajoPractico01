﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class BookingState
{
    public int BookingStateId { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}