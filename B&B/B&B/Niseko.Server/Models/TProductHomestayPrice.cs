﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TProductHomestayPrice
{
    public int FHomestayPriceID { get; set; }

    public int FHomestayRoomID { get; set; }

    public bool FIsPeakSeason { get; set; }

    public byte FStayDays { get; set; }

    public decimal FStayPrice { get; set; }

    public virtual TProductHomestayRoom FHomestayRoom { get; set; }
}