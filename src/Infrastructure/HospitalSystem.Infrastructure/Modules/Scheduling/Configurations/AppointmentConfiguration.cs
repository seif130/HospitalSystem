using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Scheduling.Configurations
{
    internal sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(id => id.Value, value => new AppointmentId(value))
                .ValueGeneratedNever();

            builder.Property(a => a.PatientId)
                .HasConversion(id => id.Value, value => new PatientId(value))
                .IsRequired();

            builder.Property(a => a.DoctorId)
                .HasConversion(id => id.Value, value => new DoctorId(value))
                .IsRequired();

            builder.OwnsOne(a => a.Slot, slot =>
            {
                slot.Property(d => d.Start).HasColumnName("StartUtc").IsRequired();
                slot.Property(d => d.End).HasColumnName("EndUtc").IsRequired();
            });

            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.BookedOnUtc).IsRequired();
            builder.Property(a => a.CancellationReason).HasMaxLength(500);
        }
    }
}
