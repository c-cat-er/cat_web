﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace modpackFront.Models;

public partial class OrderDetail
{
    public int DetailsId { get; set; }

    public int OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? InspirationId { get; set; }

    public int? CustomizedId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public virtual Customized Customized { get; set; }

    public virtual Inspiration Inspiration { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}