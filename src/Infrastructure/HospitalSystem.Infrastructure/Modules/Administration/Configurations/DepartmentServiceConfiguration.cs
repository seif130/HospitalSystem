using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class DepartmentServiceConfiguration : IEntityTypeConfiguration<DepartmentService>
    {
        public void Configure(EntityTypeBuilder<DepartmentService> builder)
        {
           
            builder.ToTable("DepartmentServices", "Administration");

            builder.HasKey(ds => ds.Id);

            builder.Property(ds => ds.ServiceName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(ds => ds.Description)
                .HasMaxLength(500);

            builder.OwnsOne(ds => ds.Price, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName("PriceAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                moneyBuilder.Property(m => m.Currency)
                    .HasColumnName("PriceCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });


            builder.HasOne(ds => ds.Department)
                .WithMany(d => d.Services)
                .HasForeignKey(ds => ds.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
