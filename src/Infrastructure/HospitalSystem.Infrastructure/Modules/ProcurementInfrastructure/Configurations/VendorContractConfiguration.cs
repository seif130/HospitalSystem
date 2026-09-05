using HospitalSystem.Domain.Modules.Procurement.VendorContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Configurations;

internal sealed class VendorContractConfiguration : IEntityTypeConfiguration<VendorContract>
{
    public void Configure(EntityTypeBuilder<VendorContract> builder)
    {
        builder.ToTable("VendorContracts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(StrongIdValueConverters.VendorContractId()).ValueGeneratedNever();
        builder.Property(x => x.VendorId).HasConversion(StrongIdValueConverters.VendorId()).IsRequired();
        builder.Property(x => x.Category).HasConversion<int>().IsRequired();
        builder.Property(x => x.Status).HasConversion<int>().IsRequired();

        builder.OwnsOne(x => x.Term, term =>
        {
            term.Property(x => x.Start).HasColumnName("TermStart").IsRequired();
            term.Property(x => x.End).HasColumnName("TermEnd");
            term.HasIndex(x => x.Start);
            term.HasIndex(x => x.End);
        });

        builder.OwnsOne(x => x.ContractValue, money =>
        {
            money.Property(x => x.Amount).HasColumnName("ContractValueAmount").HasPrecision(19, 4).IsRequired();
            money.Property(x => x.Currency).HasColumnName("ContractValueCurrency").HasMaxLength(3).IsRequired();
        });

        builder.HasIndex(x => new { x.VendorId, x.Status });
    }
}
