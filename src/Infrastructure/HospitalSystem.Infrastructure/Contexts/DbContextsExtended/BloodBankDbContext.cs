using HospitalSystem.Domain.Modules.BloodBank.BloodDonor;
using HospitalSystem.Domain.Modules.BloodBank.BloodRequest;
using HospitalSystem.Domain.Modules.BloodBank.BloodTransfusion;
using HospitalSystem.Domain.Modules.BloodBank.BloodUnit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsExtended
{
    public sealed class BloodBankDbContext : DbContext
    {
        public BloodBankDbContext(DbContextOptions<BloodBankDbContext> options) : base(options) { }

        public DbSet<BloodDonor> BloodDonors => Set<BloodDonor>();
        public DbSet<BloodUnit> BloodUnits => Set<BloodUnit>();
        public DbSet<BloodRequest> BloodRequests => Set<BloodRequest>();
        public DbSet<BloodTransfusion> BloodTransfusions => Set<BloodTransfusion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BloodBankDbContext).Assembly);
    }
}
