﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebAppDemo.ServiceDefaults.Models;

public partial class System
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Guid? BusinessStructureId { get; set; }

    public bool IsImportSystem { get; set; }

    public bool IsExportSystem { get; set; }

    public bool AllowPricesToBeChanged { get; set; }

    public int? JobTypeId { get; set; }

    public bool HasHistoricPrices { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; }

    public DateTime Modified { get; set; }

    public string ModifiedBy { get; set; }
}