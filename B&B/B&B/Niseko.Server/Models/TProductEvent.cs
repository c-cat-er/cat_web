﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TProductEvent
{
    public int FEventID { get; set; }

    public bool FIsPublished { get; set; }

    public byte FVendorID { get; set; }

    public string FEventCode { get; set; }

    public string FEventName { get; set; }

    public DateTime? FStartDatetime { get; set; }

    public DateTime? FEndDatetime { get; set; }

    public decimal FEventPrice { get; set; }

    public virtual TVendor FVendor { get; set; }
}