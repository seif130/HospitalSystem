using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using HospitalSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Configurations.Scheduling
{
    public sealed class ClinicRoomConfiguration: IEntityTypeConfiguration<ClinicRoom>
    {
        public void Configure(EntityTypeBuilder<ClinicRoom> builder)
        {
            builder.ToTable("ClinicRooms");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ClinicRoomId(value))
                .ValueGeneratedNever();

            builder.Property(x => x.RoomNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.DepartmentId)
                .HasConversion(
                    id => id.Value,
                    value => new DepartmentId(value))
                .IsRequired();

            builder.Property(x => x.Capacity)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.DepartmentId,
                x.RoomNumber
            })
            .IsUnique();

            // Room Bookings

            builder.OwnsMany<DateRange>(
                "_bookings",
                booking =>
                {
                    booking.ToTable("ClinicRoomBookings");

                    booking.WithOwner()
                        .HasForeignKey("ClinicRoomId");

                    booking.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    booking.HasKey("Id");

                    booking.Property(x => x.Start)
                        .HasColumnName("StartUtc")
                        .IsRequired();

                    booking.Property(x => x.End)
                        .HasColumnName("EndUtc");
                });

            builder.Navigation("_bookings")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }

}
