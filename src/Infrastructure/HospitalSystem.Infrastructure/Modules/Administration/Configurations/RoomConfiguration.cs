using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms", "Administration");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(r => r.Department)
                .WithMany(d => d.Rooms)
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Beds)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(r => r.Beds)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
