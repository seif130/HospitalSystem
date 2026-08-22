using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Configurations.Scheduling
{
    public sealed class WaitlistConfiguration : IEntityTypeConfiguration<Waitlist>
    {
        public void Configure(EntityTypeBuilder<Waitlist> builder)
        {
            builder.ToTable("Waitlists");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new WaitlistId(value))
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

            builder.Property(x => x.OfferedAppointmentId)
                .HasConversion(
                    id => id.Value,
                    value => new AppointmentId(value));

            builder.Property(x => x.PreferredFromUtc)
                .IsRequired();

            builder.Property(x => x.PreferredToUtc)
                .IsRequired();

            builder.Property(x => x.JoinedOnUtc)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.DoctorId,
                x.Status,
                x.PreferredFromUtc
            });

            builder.HasIndex(x => new
            {
                x.PatientId,
                x.DoctorId,
                x.Status
            });
        }
    }

}
