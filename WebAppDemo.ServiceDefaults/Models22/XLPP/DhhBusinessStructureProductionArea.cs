﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemo.ServiceDefaults.Models22;

[Table("dhh_BusinessStructureProductionAreas", Schema = "XLPP")]
[Index("BusinessStructureId", "ProductionAreaId", Name = "BusinessStructureProductionArea_Uniqueness", IsUnique = true)]
public partial class DhhBusinessStructureProductionArea
{
    [Key]
    public Guid Id { get; set; }

    public Guid BusinessStructureId { get; set; }

    public Guid ProductionAreaId { get; set; }
}