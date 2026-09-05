using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Configurations.Scheduling
{
    public sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new DoctorId(value))
                .ValueGeneratedNever();

            builder.Property(x => x.DepartmentId)
                .HasConversion(
                    id => id.Value,
                    value => new DepartmentId(value))
                .IsRequired();

            builder.Property(x => x.Specialty)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LicenseNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.LicenseNumber)
                .IsUnique();

            builder.OwnsOne(
                x => x.Name,
                name =>
                {
                    name.Property(x => x.FirstName)
                        .HasColumnName("FirstName")
                        .HasMaxLength(100)
                        .IsRequired();

                    name.Property(x => x.MiddleName)
                        .HasColumnName("MiddleName")
                        .HasMaxLength(100);

                    name.Property(x => x.LastName)
                        .HasColumnName("LastName")
                        .HasMaxLength(100)
                        .IsRequired();
                });
        }
    }


}
