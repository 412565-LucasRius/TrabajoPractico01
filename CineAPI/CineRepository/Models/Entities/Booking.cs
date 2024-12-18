﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CineRepository.Models.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? BookingDate { get; set; }

    public int? BookingStateId { get; set; }

    public virtual BookingState BookingState { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<PaymentMethodsBooking> PaymentMethodsBookings { get; set; } = new List<PaymentMethodsBooking>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}