﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TTagType
{
    public byte FTagTypeID { get; set; }

    public string FTypeName { get; set; }

    public virtual ICollection<TTag> TTags { get; set; } = new List<TTag>();
}