using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Clinic.Prescriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.ClinicalAndInpatient.Configrations
{
    internal sealed class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, value => new PrescriptionId(value))
                .ValueGeneratedNever();

            builder.Property(p => p.PatientId)
                .HasConversion(id => id.Value, value => new PatientId(value))
                .IsRequired();

            builder.Property(p => p.PrescribedByDoctorId)
                .HasConversion(id => id.Value, value => new DoctorId(value))
                .IsRequired();

            builder.Property(p => p.IssuedOnUtc).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            builder.OwnsMany(p => p.Items, item =>
            {
                item.ToTable("PrescriptionItems");
                item.WithOwner().HasForeignKey("PrescriptionId");
                item.Property<int>("Id");
                item.HasKey("Id");

                item.Property(i => i.MedicineId)
                    .HasConversion(id => id.Value, value => new MedicineId(value))
                    .IsRequired();

                item.OwnsOne(i => i.Dosage, dosage =>
                {
                    dosage.Property(d => d.Amount).HasColumnName("DosageAmount").IsRequired();
                    dosage.Property(d => d.Unit).HasColumnName("DosageUnit").HasMaxLength(20).IsRequired();
                    dosage.Property(d => d.Frequency).HasColumnName("DosageFrequency").HasMaxLength(50).IsRequired();
                });

                item.Property(i => i.Instructions).HasMaxLength(500).IsRequired();
                item.Property(i => i.DurationInDays).IsRequired();
            });
        }
    }
}
