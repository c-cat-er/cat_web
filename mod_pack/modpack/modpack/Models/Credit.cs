﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace modpack.Models;

public partial class Credit
{
    public int CreditId { get; set; }

    public int? MemberId { get; set; }

    public string HistoryName { get; set; }

    public int? IncomingAmount { get; set; }

    public int? UsageAmount { get; set; }

    public DateTime ModificationTime { get; set; }

    public virtual Member Member { get; set; }
}