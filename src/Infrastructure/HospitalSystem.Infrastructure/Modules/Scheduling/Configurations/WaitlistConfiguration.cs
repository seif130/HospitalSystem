using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Scheduling.Configurations
{
    internal sealed class WaitlistConfiguration : IEntityTypeConfiguration<Waitlist>
    {
        public void Configure(EntityTypeBuilder<Waitlist> builder)
        {
            builder.ToTable("Waitlists");
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .HasConversion(id => id.Value, value => new WaitlistId(value))
                .ValueGeneratedNever();

            builder.Property(w => w.PatientId)
                .HasConversion(id => id.Value, value => new PatientId(value))
                .IsRequired();

            builder.Property(w => w.DoctorId)
                .HasConversion(id => id.Value, value => new DoctorId(value))
                .IsRequired();

            builder.Property(w => w.OfferedAppointmentId)
                .HasConversion(
                id => id != null ? id.Value : (Guid?)null,
                value => value.HasValue ? new AppointmentId(value.Value) : null);

            builder.Property(w => w.PreferredFromUtc).IsRequired();
            builder.Property(w => w.PreferredToUtc).IsRequired();
            builder.Property(w => w.Status).IsRequired();
            builder.Property(w => w.JoinedOnUtc).IsRequired();
        }
    }
}
