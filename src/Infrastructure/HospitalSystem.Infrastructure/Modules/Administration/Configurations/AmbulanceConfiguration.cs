using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class AmbulanceConfiguration : IEntityTypeConfiguration<Ambulance>
    {
        public void Configure(EntityTypeBuilder<Ambulance> builder)
        {
            // تخصيص اسم الجدول والـ Schema
            builder.ToTable("Ambulances", "Administration");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.VehicleNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(a => a.VehicleNumber)
                .IsUnique();

            builder.Property(a => a.DriverName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a =>a.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}
