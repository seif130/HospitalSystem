using HospitalSystem.Domain.Modules.Emergency.Ambulance;
using HospitalSystem.Domain.Modules.Emergency.AmbulanceDispatch;
using HospitalSystem.Domain.Modules.Emergency.EmergencyCase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsExtended
{
    public sealed class EmergencyDbContext : DbContext
    {
        public EmergencyDbContext(DbContextOptions<EmergencyDbContext> options) : base(options) { }

        public DbSet<EmergencyCase> EmergencyCases => Set<EmergencyCase>();
        public DbSet<Ambulance> Ambulances => Set<Ambulance>();
        public DbSet<AmbulanceDispatch> AmbulanceDispatches => Set<AmbulanceDispatch>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmergencyDbContext).Assembly);
    }
}
