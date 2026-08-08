using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors", "Administration");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(d => d.Email)
                .IsUnique();

            builder.Property(d => d.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(d => d.Specialization)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(d => d.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(d => d.Department)
                .WithMany() 
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
