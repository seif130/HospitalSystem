using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract
{
    public interface IDoctorScheduleRepository
        : IRepository<DoctorSchedule, DoctorScheduleId>
    {
        Task<IReadOnlyList<DoctorSchedule>> GetByDoctorAsync(
            DoctorId doctorId,
            CancellationToken ct = default);

        Task<bool> HasConflictAsync(
            DoctorId doctorId,
            DayOfWeek dayOfWeek,
            TimeSpan startTime,
            TimeSpan endTime,
            DoctorScheduleId? excludeId = null,
            CancellationToken ct = default);
    }
}
