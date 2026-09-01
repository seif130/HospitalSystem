
using HospitalSystem.Domain.Modules.Compliance.AuditLog;
using HospitalSystem.Domain.Modules.Compliance.ConsentRecord;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsCrossCutting
{

    public sealed class ComplianceDbContext : DbContext
    {
        public ComplianceDbContext(DbContextOptions<ComplianceDbContext> options) : base(options) { }

        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<ConsentRecord> ConsentRecords => Set<ConsentRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComplianceDbContext).Assembly);
    }
}
