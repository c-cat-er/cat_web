﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace NunitTrain1.Models;

public partial class Customer
{
    public int cid { get; set; }

    public string name { get; set; }

    public string email { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}