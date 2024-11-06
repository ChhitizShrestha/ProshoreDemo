﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebAppDemo.ServiceDefaults.Models;

public partial class BusinessUnit
{
    public int Id { get; set; }

    public Guid BusinessStructureId { get; set; }

    public int? ImportSystemId { get; set; }

    public int? ExportSystemId { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; }

    public DateTime Modified { get; set; }

    public string ModifiedBy { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}