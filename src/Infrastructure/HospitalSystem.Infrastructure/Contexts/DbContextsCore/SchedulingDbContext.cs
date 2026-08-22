using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using HospitalSystem.Domain.Modules.Scheduling.Departments;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Specialties;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using HospitalSystem.Domain.Reprository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsCore
{
    public sealed class SchedulingDbContext : DbContext, IUnitOfWork
    {
        public SchedulingDbContext( DbContextOptions<SchedulingDbContext> options): base(options)
        {
        }

        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<ClinicRoom> ClinicRooms => Set<ClinicRoom>();
        public DbSet<Specialty> Specialties => Set<Specialty>();
        public DbSet<Waitlist> Waitlists => Set<Waitlist>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingDbContext).Assembly);
        }


    }

}
