﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TOrderDetailCourse
{
    public int FOrderDetailCourseID { get; set; }

    public int FOrderDetailID { get; set; }

    public byte FLocationID { get; set; }

    public byte FDays { get; set; }

    public byte FPeopleCount { get; set; }

    public virtual TLocation FLocation { get; set; }

    public virtual TOrderDetail FOrderDetail { get; set; }
}