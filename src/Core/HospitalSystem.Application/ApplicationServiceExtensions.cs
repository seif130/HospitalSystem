using FluentValidation;
using HospitalSystem.Application.Shared.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HospitalSystem.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddSchedulingApplication(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationServiceExtensions).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            });

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}