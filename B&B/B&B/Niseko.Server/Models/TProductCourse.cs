﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TProductCourse
{
    public int FCourseID { get; set; }

    public bool FIsPublished { get; set; }

    public string FCourseCode { get; set; }

    public string FCourseName { get; set; }

    public virtual ICollection<TProductCoursePrice> TProductCoursePrices { get; set; } = new List<TProductCoursePrice>();
}