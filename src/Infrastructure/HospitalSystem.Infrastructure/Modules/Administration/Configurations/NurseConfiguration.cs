using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.ToTable("Nurses", "Administration");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(100);
       
            builder.Property(n => n.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(n => n.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(n => n.Email)
                .IsUnique();

            builder.Property(n => n.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(n => n.Shift)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(n => n.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(n => n.Department)
                .WithMany()
                .HasForeignKey(n => n.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
