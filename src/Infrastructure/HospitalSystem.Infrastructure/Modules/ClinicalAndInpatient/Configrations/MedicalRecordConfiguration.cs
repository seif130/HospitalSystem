using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Clinic.MedicalRecords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.ClinicalAndInpatient.Configrations
{
    internal sealed class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.ToTable("MedicalRecords");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasConversion(id => id.Value, value => new MedicalRecordId(value))
                .ValueGeneratedNever();

            builder.Property(m => m.PatientId)
                .HasConversion(id => id.Value, value => new PatientId(value))
                .IsRequired();

            // Diagnoses (Owned Collection)
            builder.OwnsMany(m => m.Diagnoses, diag =>
            {
                diag.ToTable("MedicalRecordDiagnoses");
                diag.WithOwner().HasForeignKey("MedicalRecordId");
                diag.Property<int>("Id");
                diag.HasKey("Id");

                diag.Property(d => d.Code).HasMaxLength(20).IsRequired();
                diag.Property(d => d.Description).HasMaxLength(500).IsRequired();
                diag.Property(d => d.DiagnosedOnUtc).IsRequired();
            });

            // Clinical Notes (Owned Collection)
            builder.OwnsMany(m => m.Notes, note =>
            {
                note.ToTable("MedicalRecordNotes");
                note.WithOwner().HasForeignKey("MedicalRecordId");
                note.Property<int>("Id");
                note.HasKey("Id");

                note.Property(n => n.AuthorName).HasMaxLength(100).IsRequired();
                note.Property(n => n.Text).HasMaxLength(2000).IsRequired();
                note.Property(n => n.WrittenOnUtc).IsRequired();
            });

            // Vital Signs (Owned Collection)
            builder.OwnsMany(m => m.VitalSigns, vs =>
            {
                vs.ToTable("MedicalRecordVitalSigns");
                vs.WithOwner().HasForeignKey("MedicalRecordId");
                vs.Property<int>("Id");
                vs.HasKey("Id");

                vs.Property(v => v.Temperature).HasColumnType("decimal(4,2)").IsRequired();
                vs.Property(v => v.SystolicBp).IsRequired();
                vs.Property(v => v.DiastolicBp).IsRequired();
                vs.Property(v => v.PulseBpm).IsRequired();
                vs.Property(v => v.RecordedOnUtc).IsRequired();
            });

            // Allergies (Owned Collection)
            builder.OwnsMany(m => m.Allergies, allergy =>
            {
                allergy.ToTable("MedicalRecordAllergies");
                allergy.WithOwner().HasForeignKey("MedicalRecordId");
                allergy.Property<int>("Id");
                allergy.HasKey("Id");

                allergy.Property(a => a.Allergen).HasMaxLength(100).IsRequired();
                allergy.Property(a => a.Severity).IsRequired();
                allergy.Property(a => a.Reaction).HasMaxLength(300);
                allergy.Property(a => a.RecordedOnUtc).IsRequired();
            });

            // Immunizations (Owned Collection)
            builder.OwnsMany(m => m.Immunizations, imm =>
            {
                imm.ToTable("MedicalRecordImmunizations");
                imm.WithOwner().HasForeignKey("MedicalRecordId");
                imm.Property<int>("Id");
                imm.HasKey("Id");

                imm.Property(i => i.VaccineName).HasMaxLength(100).IsRequired();
                imm.Property(i => i.AdministeredOnUtc).IsRequired();
                imm.Property(i => i.AdministeredByStaffId).HasMaxLength(50).IsRequired();
                imm.Property(i => i.NextDoseDueUtc);
            });
        }
    }
}
