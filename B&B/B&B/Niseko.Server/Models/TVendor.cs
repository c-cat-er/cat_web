﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TVendor
{
    public byte FVendorID { get; set; }

    public bool FIsContact { get; set; }

    public string FVendorCode { get; set; }

    public string FVendorName { get; set; }

    public string FContactName { get; set; }

    public string FContactPhone { get; set; }

    public virtual ICollection<TProductEquipment> TProductEquipments { get; set; } = new List<TProductEquipment>();

    public virtual ICollection<TProductEvent> TProductEvents { get; set; } = new List<TProductEvent>();
}