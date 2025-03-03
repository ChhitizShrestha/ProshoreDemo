﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemo.ServiceDefaults.Models22;

[Table("JobType", Schema = "XLPP")]
[Index("Name", Name = "UQ_XLPP_JobType_Name", IsUnique = true)]
public partial class JobType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [InverseProperty("JobType")]
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}