using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class DepartmentEquipmentConfiguration : IEntityTypeConfiguration<DepartmentEquipment>
    {
        public void Configure(EntityTypeBuilder<DepartmentEquipment> builder)
        {

            builder.ToTable("DepartmentEquipments", "Administration");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.EquipmentName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.SerialNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(e => e.SerialNumber)
                .IsUnique();

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(e => e.PurchaseDate)
                .IsRequired();

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Equipments)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
