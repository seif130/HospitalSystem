using HospitalSystem.Application.Shared.Abstractions;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using HospitalSystem.Infrastructure.Modules.Scheduling.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure
{
    public static class SchedulingInfrastructureExtensions
    {
        public static IServiceCollection AddSchedulingInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SchedulingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork>(provider =>
                provider.GetRequiredService<SchedulingDbContext>());

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IClinicRoomRepository, ClinicRoomRepository>();
            services.AddScoped<IWaitlistRepository, WaitlistRepository>();

            return services;
        }
    }
}
