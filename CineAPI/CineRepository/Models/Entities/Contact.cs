﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CineRepository.Models.Entities;

public partial class Contact
{
    public int ContactId { get; set; }

    public int? CustomerId { get; set; }

    public int? ContactTypeId { get; set; }

    public string Contact1 { get; set; }

    public virtual ContactType ContactType { get; set; }

    public virtual Customer Customer { get; set; }
}