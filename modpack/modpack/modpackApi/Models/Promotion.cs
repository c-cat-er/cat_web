﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace modpackApi.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Customized> Customizeds { get; set; } = new List<Customized>();

    public virtual ICollection<Inspiration> Inspirations { get; set; } = new List<Inspiration>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}