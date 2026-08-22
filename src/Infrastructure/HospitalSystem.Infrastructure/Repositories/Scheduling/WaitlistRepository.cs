using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Enums;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Repositories.Scheduling
{
    public sealed class WaitlistRepository: Repository<Waitlist, WaitlistId>,IWaitlistRepository
    {
        public WaitlistRepository(SchedulingDbContext context): base(context)
        {
        }

        public async Task<IReadOnlyList<Waitlist>> GetWaitingByDoctorAsync(
            DoctorId doctorId,DateRange period,CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(period);

            return await DbSet
                .AsNoTracking()
                .Where(x =>x.DoctorId == doctorId &&
                    x.Status == WaitlistEntryStatus.Waiting &&
                    x.PreferredFromUtc < (period.End ?? DateTime.MaxValue) && period.Start < x.PreferredToUtc)
                .OrderBy(x => x.JoinedOnUtc)
                .ToListAsync(ct);
        }

        public Task<bool> HasActiveEntryAsync(PatientId patientId,DoctorId doctorId,
            CancellationToken ct = default)
        {
            return DbSet.AnyAsync(
                x =>x.PatientId == patientId &&
                    x.DoctorId == doctorId &&
                    x.Status == WaitlistEntryStatus.Waiting,ct);
        }
    }



}
