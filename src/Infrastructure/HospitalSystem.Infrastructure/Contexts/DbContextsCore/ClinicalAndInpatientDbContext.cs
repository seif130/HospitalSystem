
using HospitalSystem.Domain.Modules.Clinic.Admissions;
using HospitalSystem.Domain.Modules.Clinic.DischargeSummaries;
using HospitalSystem.Domain.Modules.Clinic.MedicalRecords;
using HospitalSystem.Domain.Modules.Clinic.Nurses;
using HospitalSystem.Domain.Modules.Clinic.Patients;
using HospitalSystem.Domain.Modules.Clinic.Surgeries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsCore
{
    public sealed class ClinicalAndInpatientDbContext : DbContext
    {
        public ClinicalAndInpatientDbContext(DbContextOptions<ClinicalAndInpatientDbContext> options) : base(options) { }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Nurse> Nurses => Set<Nurse>();
        public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
        public DbSet<Admission> Admissions => Set<Admission>();
        public DbSet<Surgery> Surgeries => Set<Surgery>();
        public DbSet<DischargeSummary> DischargeSummaries => Set<DischargeSummary>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicalAndInpatientDbContext).Assembly);
    }
}
