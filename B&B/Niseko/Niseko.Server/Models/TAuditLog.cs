﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TAuditLog
{
    public int FAuditID { get; set; }

    public string FTableName { get; set; }

    public string FActionType { get; set; }

    public DateTime FActionDatetime { get; set; }

    public int FUserID { get; set; }

    public string FOldValue { get; set; }

    public string FNewValue { get; set; }
}