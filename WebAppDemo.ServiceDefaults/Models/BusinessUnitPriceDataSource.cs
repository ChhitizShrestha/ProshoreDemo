﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebAppDemo.ServiceDefaults.Models;

public partial class BusinessUnitPriceDataSource
{
    public int Id { get; set; }

    public int? BusinessUnitId { get; set; }

    public int? PriceTypeId { get; set; }

    public int? DataSourceId { get; set; }

    public string ExportFilepath { get; set; }

    public string Notification { get; set; }
}