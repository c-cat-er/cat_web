﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace modpackApi.Models;

public partial class ServiceRecord
{
    public int RecordId { get; set; }

    public int MemberId { get; set; }

    public string Question { get; set; }

    public DateTime? QuestionTime { get; set; }

    public int? AdministratorId { get; set; }

    public string Answer { get; set; }

    public DateTime? AnswerTime { get; set; }

    public bool IsResolved { get; set; }

    public virtual Administrator Administrator { get; set; }

    public virtual Member Member { get; set; }
}