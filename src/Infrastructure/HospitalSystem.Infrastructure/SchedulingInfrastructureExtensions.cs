using HospitalSystem.Infrastructure.Contexts.DbContextsCore;

namespace HospitalSystem.Infrastructure
{
    using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
    using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
    using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
    using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
    using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
    using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
    using HospitalSystem.Domain.Reprository;
    using HospitalSystem.Infrastructure.Repositories.Scheduling;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class SchedulingInfrastructureExtensions
    {
        public static IServiceCollection AddSchedulingInfrastructure(
            this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not found.");

            services.AddDbContext<SchedulingDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork>(provider =>
                provider.GetRequiredService<SchedulingDbContext>());

            // Repositories
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IClinicRoomRepository, ClinicRoomRepository>();
            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            services.AddScoped<IWaitlistRepository, WaitlistRepository>();

            return services;
        }
    }

}
