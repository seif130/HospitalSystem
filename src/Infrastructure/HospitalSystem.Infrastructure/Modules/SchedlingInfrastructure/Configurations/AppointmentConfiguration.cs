using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Configurations.Scheduling
{
    public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new AppointmentId(value))
                .ValueGeneratedNever();

            builder.Property(x => x.PatientId)
                .HasConversion(
                    id => id.Value,
                    value => new PatientId(value))
                .IsRequired();

            builder.Property(x => x.DoctorId)
                .HasConversion(
                    id => id.Value,
                    value => new DoctorId(value))
                .IsRequired();

            builder.Property(x => x.ClinicRoomId)
                .HasConversion(
                    id => id.Value,
                    value => new ClinicRoomId(value))
                .IsRequired();

            builder.OwnsOne(
                x => x.ScheduledPeriod,
                period =>
                {
                    period.Property(x => x.Start)
                        .HasColumnName("ScheduledStartUtc")
                        .IsRequired();

                    period.Property(x => x.End)
                        .HasColumnName("ScheduledEndUtc");
                });

            builder.Property(x => x.Type)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Reason)
                .HasMaxLength(500);

            builder.Property(x => x.CancellationReason)
                .HasMaxLength(500);

            builder.HasIndex(x => new
            {
                x.DoctorId,
                x.ScheduledPeriod
            });

            builder.HasIndex(x => new
            {
                x.PatientId,
                x.ScheduledPeriod
            });

            builder.HasIndex(x => new
            {
                x.ClinicRoomId,
                x.ScheduledPeriod
            });
        }
    }

}
