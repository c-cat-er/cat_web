﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TEmployee
{
    public int FEmployeeID { get; set; }

    public string FEmployeeCode { get; set; }

    public string FFirstName { get; set; }

    public string FLastName { get; set; }

    public byte FDepartmentID { get; set; }

    public string FPosition { get; set; }

    public DateOnly FHireDate { get; set; }

    public DateOnly? FFireDate { get; set; }

    public DateOnly FBirthDate { get; set; }

    public string FPhone { get; set; }

    public string FEmail { get; set; }

    public string FAccount { get; set; }

    public byte[] FPasswordHash { get; set; }

    public byte[] FPasswordSalt { get; set; }

    public byte FPermissions { get; set; }

    public virtual TDepartment FDepartment { get; set; }

    public virtual ICollection<TEmployeeLoginRecord> TEmployeeLoginRecords { get; set; } = new List<TEmployeeLoginRecord>();
}