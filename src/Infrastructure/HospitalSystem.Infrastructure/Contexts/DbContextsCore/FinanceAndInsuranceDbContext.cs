
using HospitalSystem.Domain.Modules.FinanceAndInsurance.DiscountAdjustment;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.InsuranceClaim;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.InsurancePolicy;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.InsuranceProvider;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.Invoice;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.Payment;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.Refund;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsCore
{
    public sealed class FinanceAndInsuranceDbContext : DbContext
    {
        public FinanceAndInsuranceDbContext(DbContextOptions<FinanceAndInsuranceDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<InsuranceClaim> InsuranceClaims => Set<InsuranceClaim>();
        public DbSet<InsuranceProvider> InsuranceProviders => Set<InsuranceProvider>();
        public DbSet<InsurancePolicy> InsurancePolicies => Set<InsurancePolicy>();
        public DbSet<Refund> Refunds => Set<Refund>();
        public DbSet<DiscountAdjustment> DiscountAdjustments => Set<DiscountAdjustment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceAndInsuranceDbContext).Assembly);
    }
}
