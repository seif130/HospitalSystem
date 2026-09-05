using HospitalSystem.Domain.Modules.Procurement.Budgets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Configurations;

internal sealed class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(StrongIdValueConverters.BudgetId()).ValueGeneratedNever();
        builder.Property(x => x.DepartmentId).HasConversion(StrongIdValueConverters.DepartmentId()).IsRequired();

        builder.OwnsOne(x => x.FiscalPeriod, period =>
        {
            period.Property(x => x.Start).HasColumnName("FiscalStart").IsRequired();
            period.Property(x => x.End).HasColumnName("FiscalEnd");
            period.HasIndex(x => x.Start);
            period.HasIndex(x => x.End);
        });

        builder.OwnsOne(x => x.AllocatedAmount, money =>
        {
            money.Property(x => x.Amount).HasColumnName("AllocatedAmount").HasPrecision(19, 4).IsRequired();
            money.Property(x => x.Currency).HasColumnName("AllocatedCurrency").HasMaxLength(3).IsRequired();
        });

        builder.OwnsMany(x => x.Expenses, expense =>
        {
            expense.ToTable("BudgetExpenses");
            expense.WithOwner().HasForeignKey("BudgetId");
            expense.Property<Guid>("Id").ValueGeneratedOnAdd();
            expense.HasKey("Id");
            expense.Property(x => x.Description).HasMaxLength(500).IsRequired();
            expense.Property(x => x.IncurredOnUtc).IsRequired();
            expense.OwnsOne(x => x.Amount, money =>
            {
                money.Property(x => x.Amount).HasColumnName("Amount").HasPrecision(19, 4).IsRequired();
                money.Property(x => x.Currency).HasColumnName("Currency").HasMaxLength(3).IsRequired();
            });
        });

        builder.HasIndex(x => new { x.DepartmentId });
        builder.Navigation(x => x.Expenses).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
