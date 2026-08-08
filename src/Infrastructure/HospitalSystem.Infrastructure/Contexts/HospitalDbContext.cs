using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts
{

    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Bed> Beds => Set<Bed>();
        public DbSet<Ambulance> Ambulances => Set<Ambulance>();
        public DbSet<DepartmentEquipment> DepartmentEquipments => Set<DepartmentEquipment>();
        public DbSet<DepartmentService> DepartmentServices => Set<DepartmentService>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Nurse> Nurses => Set<Nurse>();
        public DbSet<OnCallSchedule> OnCallSchedules => Set<OnCallSchedule>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalDbContext).Assembly);
        }
    }
}
