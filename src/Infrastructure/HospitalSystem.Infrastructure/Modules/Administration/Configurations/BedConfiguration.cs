using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class BedConfiguration : IEntityTypeConfiguration<Bed>
    {
        public void Configure(EntityTypeBuilder<Bed> builder)
        {
            // تخصيص اسم الجدول والـ Schema
            builder.ToTable("Beds", "Administration");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.BedNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
