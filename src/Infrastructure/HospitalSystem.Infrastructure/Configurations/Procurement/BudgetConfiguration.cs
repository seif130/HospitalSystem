using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Configurations.Procurement
{
    public sealed class BudgetConfiguration
      : IEntityTypeConfiguration<Budgets>
    {
        public void Configure(
            EntityTypeBuilder<Budgets> builder)
        {
            builder.ToTable("Budgets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DepartmentId)
                .HasConversion(
                    id => id.Value,
                    value => new DepartmentId(value))
                .IsRequired();

            builder.OwnsOne(
                x => x.FiscalPeriod,
                period =>
                {
                    period.Property(x => x.Start)
                        .HasColumnName("FiscalPeriodStart")
                        .IsRequired();

                    period.Property(x => x.End)
                        .HasColumnName("FiscalPeriodEnd");
                });

            builder.OwnsOne(
                x => x.AllocatedAmount,
                money =>
                {
                    money.Property(x => x.Amount)
                        .HasColumnName("AllocatedAmount")
                        .HasPrecision(18, 2)
                        .IsRequired();

                    money.Property(x => x.Currency)
                        .HasColumnName("Currency")
                        .HasMaxLength(3)
                        .IsRequired();
                });

            builder.OwnsMany(
                x => x.Expenses,
                expense =>
                {
                    expense.ToTable("BudgetExpenses");

                    expense.WithOwner()
                        .HasForeignKey("BudgetId");

                    expense.Property(x => x.Description)
                        .HasMaxLength(500)
                        .IsRequired();

                    expense.Property(x => x.IncurredOnUtc)
                        .IsRequired();

                    expense.OwnsOne(
                        x => x.Amount,
                        money =>
                        {
                            money.Property(x => x.Amount)
                                .HasColumnName("Amount")
                                .HasPrecision(18, 2)
                                .IsRequired();

                            money.Property(x => x.Currency)
                                .HasColumnName("Currency")
                                .HasMaxLength(3)
                                .IsRequired();
                        });
                });

            builder.HasIndex(x => x.DepartmentId);
        }
    }
}
