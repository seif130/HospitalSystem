using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // تخصيص اسم الجدول والـ Schema
            builder.ToTable("Departments", "Administration");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.HeadDoctorId)
                .IsRequired(false);


            builder.HasMany(d => d.Doctors)
                .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Nurses)
                .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Rooms)
                .WithOne(r => r.Department)
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Equipments)
                .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Services)
                .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Schedules)
                .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(d => d.Doctors).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(d => d.Nurses).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(d => d.Rooms).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(d => d.Equipments).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(d => d.Services).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(d => d.Schedules).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}