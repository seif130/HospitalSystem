using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Administration.Configurations
{
    public class OnCallScheduleConfiguration : IEntityTypeConfiguration<OnCallSchedule>
    {
        public void Configure(EntityTypeBuilder<OnCallSchedule> builder)
        {
            builder.ToTable("OnCallSchedules", "Administration");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.DutyDate)
                .IsRequired();

            builder.Property(o => o.Shift)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(o => o.Department)
                .WithMany()
                .HasForeignKey(o => o.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(o => o.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
