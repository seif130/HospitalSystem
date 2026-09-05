using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Configurations;

internal sealed class PurchaseRequestConfiguration : IEntityTypeConfiguration<PurchaseRequest>
{
    public void Configure(EntityTypeBuilder<PurchaseRequest> builder)
    {
        builder.ToTable("PurchaseRequests");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(StrongIdValueConverters.PurchaseRequestId()).ValueGeneratedNever();
        builder.Property(x => x.DepartmentId).HasConversion(StrongIdValueConverters.DepartmentId()).IsRequired();
        builder.Property(x => x.Reason).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.Status).HasConversion<int>().IsRequired();

        builder.OwnsMany(x => x.Lines, line =>
        {
            line.ToTable("PurchaseRequestLines");
            line.WithOwner().HasForeignKey("PurchaseRequestId");
            line.Property<Guid>("Id").ValueGeneratedOnAdd();
            line.HasKey("Id");
            line.Property(x => x.ItemName).HasMaxLength(300).IsRequired();
            line.Property(x => x.Quantity).IsRequired();
            line.OwnsOne(x => x.EstimatedUnitPrice, money =>
            {
                money.Property(x => x.Amount).HasColumnName("EstimatedUnitPriceAmount").HasPrecision(19, 4).IsRequired();
                money.Property(x => x.Currency).HasColumnName("EstimatedUnitPriceCurrency").HasMaxLength(3).IsRequired();
            });
        });

        builder.HasIndex(x => new { x.DepartmentId, x.Status });
        builder.Navigation(x => x.Lines).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
