﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public DateTime? BornDate { get; set; }

    public bool? Retired { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}