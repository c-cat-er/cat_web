﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace modpackApi.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int MemberId { get; set; }

    public int? ProductId { get; set; }

    public int? InspirationId { get; set; }

    public int? CustomizedId { get; set; }

    public int Quantity { get; set; }

    public virtual Customized Customized { get; set; }

    public virtual Inspiration Inspiration { get; set; }

    public virtual Member Member { get; set; }

    public virtual Product Product { get; set; }
}