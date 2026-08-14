using HospitalSystem.Domain.Modules.Scheduling.Specialties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Scheduling.Configurations
{
    internal sealed class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("Specialties");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Description).HasMaxLength(500);
            builder.Property(s => s.IsActive).IsRequired();
        }
    }
}
