using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Clinic.Surgeries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.ClinicalAndInpatient.Configrations
{
    internal sealed class SurgeryConfiguration : IEntityTypeConfiguration<Surgery>
    {
        public void Configure(EntityTypeBuilder<Surgery> builder)
        {
            builder.ToTable("Surgeries");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(id => id.Value, value => new SurgeryId(value))
                .ValueGeneratedNever();

            builder.Property(s => s.PatientId)
                .HasConversion(id => id.Value, value => new PatientId(value))
                .IsRequired();

            builder.Property(s => s.OperatingRoomId)
                .HasConversion(id => id.Value, value => new ClinicRoomId(value))
                .IsRequired();

            builder.Property(s => s.Procedure).HasMaxLength(200).IsRequired();
            builder.Property(s => s.ScheduledForUtc).IsRequired();
            builder.Property(s => s.Status).IsRequired();

            builder.OwnsMany(s => s.Team, team =>
            {
                team.ToTable("SurgeryTeamMembers");
                team.WithOwner().HasForeignKey("SurgeryId");
                team.Property<int>("Id");
                team.HasKey("Id");

                team.Property(t => t.StaffId)
                    .HasConversion(id => id.Value, value => new StaffId(value))
                    .IsRequired();

                team.Property(t => t.Role).IsRequired();
            });
        }
    }
}
