﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TProductShuttle
{
    public byte FShuttleID { get; set; }

    public bool FIsPublished { get; set; }

    public string FShuttleCode { get; set; }

    public string FShuttleName { get; set; }

    public byte FMaxCapacity { get; set; }

    public virtual ICollection<TProductShuttlePrice> TProductShuttlePrices { get; set; } = new List<TProductShuttlePrice>();
}