﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TMemberSkiLevel
{
    public byte FSkiLevelID { get; set; }

    public string FSkiLevelName { get; set; }

    public virtual ICollection<TMember> TMembers { get; set; } = new List<TMember>();
}