using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Repositories.Scheduling
{
    public sealed class DoctorTimeOffRepository
        : Repository<DoctorTimeOff, DoctorTimeOffId>,
          IDoctorTimeOffRepository
    {
        public DoctorTimeOffRepository(
            SchedulingDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<DoctorTimeOff>> GetByDoctorAsync(
            DoctorId doctorId,
            CancellationToken ct = default)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.DoctorId == doctorId)
                .OrderBy(x => x.Period.Start)
                .ToListAsync(ct);
        }

        public Task<bool> HasConflictAsync(
            DoctorId doctorId,
            DateRange period,
            DoctorTimeOffId? excludeId = null,
            CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(period);

            return DbSet.AnyAsync(
                x =>
                    x.DoctorId == doctorId &&
                    (excludeId == null || x.Id != excludeId) &&
                    x.Period.Overlaps(period),
                ct);
        }
    }
}
