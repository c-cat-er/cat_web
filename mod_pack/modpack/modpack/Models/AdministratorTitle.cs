﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace modpack.Models;

public partial class AdministratorTitle
{
    public int TitleId { get; set; }

    public string Name { get; set; }

    public int Permissions { get; set; }

    public virtual ICollection<Administrator> Administrators { get; set; } = new List<Administrator>();
}