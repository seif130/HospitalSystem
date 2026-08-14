using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Clinic.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.ClinicalAndInpatient.Configrations
{
    internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, value => new PatientId(value))
                .ValueGeneratedNever();

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
                name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();
            });

            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.Gender).IsRequired();
            builder.Property(p => p.BloodType);

            builder.OwnsOne(p => p.Phone, phone =>
            {
                phone.Property(p => p.Value).HasColumnName("Phone").HasMaxLength(20).IsRequired();
            });

            builder.OwnsOne(p => p.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("Email").HasMaxLength(100);
            });

            builder.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(100).IsRequired();
                address.Property(a => a.City).HasColumnName("City").HasMaxLength(50).IsRequired();
                address.Property(a => a.Country).HasColumnName("Country").HasMaxLength(50).IsRequired();
            });

            builder.Property(p => p.EmergencyContactName).HasMaxLength(100);

            builder.OwnsOne(p => p.EmergencyContactPhone, phone =>
            {
                phone.Property(p => p.Value).HasColumnName("EmergencyContactPhone").HasMaxLength(20);
            });

            builder.Property(p => p.Status).IsRequired();
        }
    }
}
