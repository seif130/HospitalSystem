using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Configurations;

internal sealed class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("PurchaseOrders");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(StrongIdValueConverters.PurchaseOrderId()).ValueGeneratedNever();
        builder.Property(x => x.VendorId).HasConversion(StrongIdValueConverters.VendorId()).IsRequired();
        builder.Property(x => x.PurchaseRequestId).HasConversion(StrongIdValueConverters.PurchaseRequestId());
        builder.Property(x => x.Status).HasConversion<int>().IsRequired();

        builder.OwnsOne(x => x.TotalAmount, money =>
        {
            money.Property(x => x.Amount).HasColumnName("TotalAmount").HasPrecision(19, 4).IsRequired();
            money.Property(x => x.Currency).HasColumnName("Currency").HasMaxLength(3).IsRequired();
        });

        builder.OwnsMany(x => x.Lines, line =>
        {
            line.ToTable("PurchaseOrderLines");
            line.WithOwner().HasForeignKey("PurchaseOrderId");
            line.Property<Guid>("Id").ValueGeneratedOnAdd();
            line.HasKey("Id");
            line.Property(x => x.ItemName).HasMaxLength(300).IsRequired();
            line.Property(x => x.Quantity).IsRequired();
            line.OwnsOne(x => x.UnitPrice, money =>
            {
                money.Property(x => x.Amount).HasColumnName("UnitPriceAmount").HasPrecision(19, 4).IsRequired();
                money.Property(x => x.Currency).HasColumnName("UnitPriceCurrency").HasMaxLength(3).IsRequired();
            });
        });

        builder.HasIndex(x => new { x.VendorId, x.Status });
        builder.HasIndex(x => new { x.PurchaseRequestId });
        builder.Navigation(x => x.Lines).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
