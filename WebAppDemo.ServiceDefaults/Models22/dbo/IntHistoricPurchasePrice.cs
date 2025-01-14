﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemo.ServiceDefaults.Models22;

[Keyless]
public partial class IntHistoricPurchasePrice
{
    [Column("HistoricPurchasePrice_Key")]
    public long? HistoricPurchasePriceKey { get; set; }

    public string ItemCode { get; set; }

    public string GlobalCode { get; set; }

    public string ProductName { get; set; }

    public string YearMonth { get; set; }

    public string PriceValue { get; set; }

    public string BusinessUnitId { get; set; }

    public string BusinessUnitName { get; set; }

    public string WeekNumber { get; set; }

    public string WeekNumberDate { get; set; }

    public string CurrencyCode { get; set; }

    public string ProductionAreaId { get; set; }

    public string ProductionAreaName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Timestamp { get; set; }
}