﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CineRepository.Models.Entities;

public partial class UserAchievement
{
    public int UserAchievementId { get; set; }

    public int? UserAccountId { get; set; }

    public int? AchievementId { get; set; }

    public DateTime? AchievedAt { get; set; }
    public virtual Achievement Achievement { get; set; }
    [JsonIgnore]
    public virtual UserAccount UserAccount { get; set; }
}