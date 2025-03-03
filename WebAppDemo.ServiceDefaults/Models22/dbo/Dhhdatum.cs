﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemo.ServiceDefaults.Models22;

[Keyless]
public partial class Dhhdatum
{
    public Guid BusinessStructureId { get; set; }

    public string BuName { get; set; }

    public Guid ProductionAreaId { get; set; }

    public string ProductionArea { get; set; }

    public Guid? PriceTypeConfigurationId { get; set; }

    [Required]
    [StringLength(256)]
    public string PriceType { get; set; }

    [Required]
    [StringLength(11)]
    [Unicode(false)]
    public string LegacyPriceType { get; set; }
}