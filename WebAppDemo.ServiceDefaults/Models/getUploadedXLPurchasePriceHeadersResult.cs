﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppDemo.ServiceDefaults.Models
{
    public partial class getUploadedXLPurchasePriceHeadersResult
    {
        public string BusinessUnitName { get; set; }
        public string PriceTypeName { get; set; }
        public string WeekNumber { get; set; }
        public DateOnly WeekNumberDate { get; set; }
        public string LegacyPriceType { get; set; }
        public Guid BusinessStructureId { get; set; }
        public Guid? PriceTypeConfigurationId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}