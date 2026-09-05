using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Infrastructure.Modules.SchedlingInfrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Extensions
{
    public static class SchedulingInfrastructureServiceExtensions
    {
        public static IServiceCollection AddSchedulingInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Scheduling")
                ?? throw new InvalidOperationException("Connection string 'Scheduling' was not found.");

            services.AddDbContext<SchedulingDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sql =>
                {
                    sql.MigrationsHistoryTable("__EFMigrationsHistory", "Scheduling");
                    sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });

            services.AddScoped<IUnitOfWork, SchedulingUnitOfWork>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IClinicRoomRepository, ClinicRoomRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IWaitlistRepository, WaitlistRepository>();

            return services;
        }
    }

}
