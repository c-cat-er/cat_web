﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TProductEquipment
{
    public int FEquipmentID { get; set; }

    public bool FIsPublished { get; set; }

    public byte FVendorID { get; set; }

    public byte FEquipmentCategoryID { get; set; }

    public string FEquipmentCode { get; set; }

    public string FEquipmentName { get; set; }

    public string FDescription { get; set; }

    public int FQuantity { get; set; }

    public decimal FEquipmentPrice { get; set; }

    public string FRemarks { get; set; }

    public virtual TProductEquipmentCategory FEquipmentCategory { get; set; }

    public virtual TVendor FVendor { get; set; }
}