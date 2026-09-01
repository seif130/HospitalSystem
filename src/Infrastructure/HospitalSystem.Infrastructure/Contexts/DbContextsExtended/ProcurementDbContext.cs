
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Domain.Modules.Procurement.VendorContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsExtended
{
    public sealed class ProcurementDbContext : DbContext
    {
        public ProcurementDbContext(DbContextOptions<ProcurementDbContext> options) : base(options) { }

        public DbSet<VendorContract> VendorContracts => Set<VendorContract>();
        public DbSet<Budgets> Budgets => Set<Budgets>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProcurementDbContext).Assembly);
    }

}
