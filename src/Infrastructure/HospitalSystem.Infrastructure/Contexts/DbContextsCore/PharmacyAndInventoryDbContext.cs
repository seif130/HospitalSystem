using HospitalSystem.Domain.Modules.PharmacyAndInventory.Batch;
using HospitalSystem.Domain.Modules.PharmacyAndInventory.InventoryItem;
using HospitalSystem.Domain.Modules.PharmacyAndInventory.Medication;
using HospitalSystem.Domain.Modules.PharmacyAndInventory.Prescription;
using HospitalSystem.Domain.Modules.PharmacyAndInventory.PurchaseOrder;
using HospitalSystem.Domain.Modules.PharmacyAndInventory.StockTransaction;
using HospitalSystem.Domain.Modules.PharmacyAndInventory.Supplier;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsCore
{
    public sealed class PharmacyAndInventoryDbContext : DbContext
    {
        public PharmacyAndInventoryDbContext(DbContextOptions<PharmacyAndInventoryDbContext> options) : base(options) { }

        public DbSet<Medication> Medications => Set<Medication>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<Batch> Batches => Set<Batch>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PharmacyAndInventoryDbContext).Assembly);
    }
}
