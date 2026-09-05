using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.SchedlingInfrastructure.Repositories
{
    public sealed class ClinicRoomRepository: Repository<ClinicRoom, ClinicRoomId>,IClinicRoomRepository
    {
        private readonly SchedulingDbContext _context;

        public ClinicRoomRepository(SchedulingDbContext context): base(context)
        {
            _context = context;
        }

        public Task<bool> ExistsByRoomNumberAsync(
            string roomNumber,
            DepartmentId departmentId,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(roomNumber))
                return Task.FromResult(false);

            var normalized = roomNumber.Trim();

            return DbSet.AnyAsync(
                x => x.RoomNumber == normalized && x.DepartmentId == departmentId,ct);
        }

        public async Task<IReadOnlyList<ClinicRoom>> GetByDepartmentAsync(
            DepartmentId departmentId,
            CancellationToken ct = default)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.DepartmentId == departmentId)
                .OrderBy(x => x.RoomNumber)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<ClinicRoom>> GetAvailableRoomsAsync(
            DepartmentId departmentId,DateRange period,
            CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(period);

            var appointments = _context.Set<Appointment>();

            return await DbSet
                .AsNoTracking()
                .Where(room =>
                    room.DepartmentId == departmentId &&
                    !appointments.Any(appointment =>
                        appointment.ClinicRoomId == room.Id &&

                        appointment.Status != AppointmentStatus.Cancelled &&
                        appointment.Status != AppointmentStatus.NoShow &&

                        appointment.ScheduledPeriod.Start < (period.End ?? DateTime.MaxValue) &&

                        period.Start <(appointment.ScheduledPeriod.End ?? DateTime.MaxValue)))
                .OrderBy(room => room.RoomNumber).ToListAsync(ct);
        }
    }

}
