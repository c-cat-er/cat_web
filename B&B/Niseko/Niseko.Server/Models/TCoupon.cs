﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TCoupon
{
    public byte FCouponID { get; set; }

    public bool FIsPublished { get; set; }

    public string FCouponCode { get; set; }

    public string FCouponName { get; set; }

    public DateOnly? FStartDate { get; set; }

    public DateOnly? FEndDate { get; set; }

    public virtual ICollection<TOrder> TOrders { get; set; } = new List<TOrder>();
}