using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Clinic.Nurses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.ClinicalAndInpatient.Configrations
{
    internal sealed class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.ToTable("Nurses");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .HasConversion(id => id.Value, value => new NurseId(value))
                .ValueGeneratedNever();

            builder.OwnsOne(n => n.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
                name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();
            });

            builder.Property(n => n.Specialization).IsRequired();

            builder.Property(n => n.DepartmentId)
                .HasConversion(id => id.Value, value => new DepartmentId(value))
                .IsRequired();

            builder.OwnsMany(n => n.Shifts, shift =>
            {
                shift.ToTable("NurseShifts");
                shift.WithOwner().HasForeignKey("NurseId");
                shift.Property<int>("Id");
                shift.HasKey("Id");

                shift.Property(s => s.Start).HasColumnName("StartUtc").IsRequired();
                shift.Property(s => s.End).HasColumnName("EndUtc").IsRequired();
            });
        }
    }
}
