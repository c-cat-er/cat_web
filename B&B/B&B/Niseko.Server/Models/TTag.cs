﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TTag
{
    public int FTagID { get; set; }

    public byte FTagTypeID { get; set; }

    public string FTagName { get; set; }

    public virtual TTagType FTagType { get; set; }

    public virtual ICollection<TProductTagMapping> TProductTagMappings { get; set; } = new List<TProductTagMapping>();
}