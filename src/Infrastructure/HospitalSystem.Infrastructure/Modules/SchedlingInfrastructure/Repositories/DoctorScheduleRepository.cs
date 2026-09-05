using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.SchedlingInfrastructure.Repositories
{
    public sealed class DoctorScheduleRepository
        : Repository<DoctorSchedule, DoctorScheduleId>,
          IDoctorScheduleRepository
    {
        public DoctorScheduleRepository(
            SchedulingDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<DoctorSchedule>> GetByDoctorAsync(
            DoctorId doctorId,
            CancellationToken ct = default)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.DoctorId == doctorId)
                .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.StartTime)
                .ToListAsync(ct);
        }

        public Task<bool> HasConflictAsync(
            DoctorId doctorId,
            DayOfWeek dayOfWeek,
            TimeSpan startTime,
            TimeSpan endTime,
            DoctorScheduleId? excludeId = null,
            CancellationToken ct = default)
        {
            return DbSet.AnyAsync(
                x =>
                    x.DoctorId == doctorId &&
                    x.DayOfWeek == dayOfWeek &&
                    (excludeId == null || x.Id != excludeId) &&
                    x.StartTime < endTime &&
                    startTime < x.EndTime,
                ct);
        }
    }
}
