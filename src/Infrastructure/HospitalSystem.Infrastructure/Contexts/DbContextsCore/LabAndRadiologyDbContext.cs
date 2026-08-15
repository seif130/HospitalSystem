using HospitalSystem.Domain.Modules.LabAndRadiology.LabOrder;
using HospitalSystem.Domain.Modules.LabAndRadiology.LabResult;
using HospitalSystem.Domain.Modules.LabAndRadiology.RadiologyOrder;
using HospitalSystem.Domain.Modules.LabAndRadiology.RadiologyReport;
using HospitalSystem.Domain.Modules.LabAndRadiology.Specimen;
using HospitalSystem.Domain.Modules.LabAndRadiology.TestCatalogItem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsCore
{
    public sealed class LabAndRadiologyDbContext : DbContext
    {
        public LabAndRadiologyDbContext(DbContextOptions<LabAndRadiologyDbContext> options) : base(options) { }

        public DbSet<LabOrder> LabOrders => Set<LabOrder>();
        public DbSet<LabResult> LabResults => Set<LabResult>();
        public DbSet<RadiologyOrder> RadiologyOrders => Set<RadiologyOrder>();
        public DbSet<RadiologyReport> RadiologyReports => Set<RadiologyReport>();
        public DbSet<TestCatalogItem> TestCatalogItems => Set<TestCatalogItem>();
        public DbSet<Specimen> Specimens => Set<Specimen>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LabAndRadiologyDbContext).Assembly);
    }
}
