using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<HospitalDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IHospitalDbContext>(provider =>
                (IHospitalDbContext)provider.GetRequiredService<HospitalDbContext>());


            return services;
        }
    }
}
