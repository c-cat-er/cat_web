﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Niseko.Server.Models;

public partial class TMemberThirdPartyAccount
{
    public int FThirdPartyAccountID { get; set; }

    public int FMemberID { get; set; }

    public byte FLoginTypeID { get; set; }

    public string FThirdPartyUniqueID { get; set; }

    public virtual TMemberLoginType FLoginType { get; set; }

    public virtual TMember FMember { get; set; }
}