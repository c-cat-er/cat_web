﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TOrder
{
    public int FOrderID { get; set; }

    public string FOrderCode { get; set; }

    public int FMemberID { get; set; }

    public DateTime FCreationDatetime { get; set; }

    public decimal FInitialAmount { get; set; }

    public byte? FDiscountCouponID { get; set; }

    public decimal FFinalAmount { get; set; }

    public bool FIsCheckedIn { get; set; }

    public virtual TCoupon FDiscountCoupon { get; set; }

    public virtual TMember FMember { get; set; }

    public virtual ICollection<TOrderDetail> TOrderDetails { get; set; } = new List<TOrderDetail>();

    public virtual ICollection<TOrderPaymentRecord> TOrderPaymentRecords { get; set; } = new List<TOrderPaymentRecord>();
}