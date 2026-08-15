using HospitalSystem.Domain.Modules.AdministrationHrPayroll.Attendance;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.EmploymentContract;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.LeaveRequest;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.Payroll;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.RoomBed;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.SalaryStructure;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.ShiftSchedule;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.Staff;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.SystemLog;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.Ward;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsCore
{
    public sealed class AdministrationHrPayrollDbContext : DbContext
    {
        public AdministrationHrPayrollDbContext(DbContextOptions<AdministrationHrPayrollDbContext> options) : base(options) { }

        public DbSet<Staff> Staff => Set<Staff>();
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<Payroll> Payrolls => Set<Payroll>();
        public DbSet<RoomBed> RoomBeds => Set<RoomBed>();
        public DbSet<SystemLog> SystemLogs => Set<SystemLog>();
        public DbSet<Ward> Wards => Set<Ward>();
        public DbSet<ShiftSchedule> ShiftSchedules => Set<ShiftSchedule>();
        public DbSet<SalaryStructure> SalaryStructures => Set<SalaryStructure>();
        public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
        public DbSet<EmploymentContract> EmploymentContracts => Set<EmploymentContract>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdministrationHrPayrollDbContext).Assembly);
    }
}
