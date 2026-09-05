using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application
{
    public static class DependencyInjection {
        public static IServiceCollection AddProcurementApplication(this IServiceCollection services)
        { 
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly); 
            return services; 
        } 
    }

}
