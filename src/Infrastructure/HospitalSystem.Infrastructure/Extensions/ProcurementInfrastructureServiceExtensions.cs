using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Contract;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Infrastructure.Modules.Procurement.Persistence;
using HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Extensions
{
    public static class ProcurementInfrastructureServiceExtensions
    {
        public static IServiceCollection AddProcurementInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProcurementConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'ProcurementConnection' was not found.");

            services.AddDbContext<ProcurementDbContext>(options =>
                options.UseSqlServer(connectionString, sql =>
                {
                    sql.MigrationsAssembly(typeof(ProcurementDbContext).Assembly.FullName);
                    sql.EnableRetryOnFailure(5);
                }));

            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IVendorContractRepository, VendorContractRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IUnitOfWork, ProcurementUnitOfWork>();

            return services;
        }
    }
}
