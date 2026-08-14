using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Clinic.Admissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.ClinicalAndInpatient.Configrations
{
    internal sealed class AdmissionConfiguration : IEntityTypeConfiguration<Admission>
    {
        public void Configure(EntityTypeBuilder<Admission> builder)
        {
            builder.ToTable("Admissions");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(id => id.Value, value =>  new AdmissionId(value))
                .ValueGeneratedNever();

            builder.Property(a => a.PatientId)
                .HasConversion(id => id.Value, value => new PatientId(value))
                .IsRequired();

            builder.Property(a => a.AttendingDoctorId)
                .HasConversion(id => id.Value, value => new DoctorId(value))
                .IsRequired();

            builder.Property(a => a.RoomBedId)
                .HasConversion(id => id.Value, value => new RoomBedId(value))
                .IsRequired();

            builder.Property(a => a.AdmittedOnUtc).IsRequired();
            builder.Property(a => a.DischargedOnUtc);
            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.DischargeSummaryText).HasMaxLength(2000);
        }
    }
}
