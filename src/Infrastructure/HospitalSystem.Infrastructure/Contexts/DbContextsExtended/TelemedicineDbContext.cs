
using HospitalSystem.Domain.Modules.Telemedicine.TelemedicinePrescription;
using HospitalSystem.Domain.Modules.Telemedicine.TelemedicineSession;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsExtended
{
    public sealed class TelemedicineDbContext : DbContext
    {
        public TelemedicineDbContext(DbContextOptions<TelemedicineDbContext> options) : base(options) { }

        public DbSet<TelemedicineSession> TelemedicineSessions => Set<TelemedicineSession>();
        public DbSet<TelemedicinePrescription> TelemedicinePrescriptions => Set<TelemedicinePrescription>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TelemedicineDbContext).Assembly);
    }
}
