using HospitalSystem.Domain;
using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using HospitalSystem.Domain.Modules.Scheduling.Departments;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Specialties;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using HospitalSystem.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Persistence;

public sealed class SchedulingDbContext(DbContextOptions<SchedulingDbContext> options) : DbContext(options)
{
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Specialty> Specialties => Set<Specialty>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<ClinicRoom> ClinicRooms => Set<ClinicRoom>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Waitlist> Waitlists => Set<Waitlist>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingDbContext).Assembly);
        modelBuilder.Ignore<IDomainEvent>();
        base.OnModelCreating(modelBuilder);
    }
}
