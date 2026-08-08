using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
  
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceExtensions).Assembly));

            services.AddValidatorsFromAssembly(typeof(ApplicationServiceExtensions).Assembly);


            return services;
        }
    }
}
