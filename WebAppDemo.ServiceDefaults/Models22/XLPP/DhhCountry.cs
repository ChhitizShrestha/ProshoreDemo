﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemo.ServiceDefaults.Models22;

[Table("dhh_Countries", Schema = "XLPP")]
public partial class DhhCountry
{
    [Key]
    public Guid Id { get; set; }

    [Column("TwoLettersISOCode")]
    [StringLength(2)]
    public string TwoLettersIsocode { get; set; }

    public string EnglishName { get; set; }
}