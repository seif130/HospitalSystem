using HospitalSystem.Domain.Modules.MultiFacility.Facility;
using HospitalSystem.Domain.Modules.MultiFacility.FacilityTransferRequest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsExtended
{
    public sealed class MultiFacilityDbContext : DbContext
    {
        public MultiFacilityDbContext(DbContextOptions<MultiFacilityDbContext> options) : base(options) { }

        public DbSet<Facility> Facilities => Set<Facility>();
        public DbSet<FacilityTransferRequest> FacilityTransferRequests => Set<FacilityTransferRequest>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MultiFacilityDbContext).Assembly);
    }
}
