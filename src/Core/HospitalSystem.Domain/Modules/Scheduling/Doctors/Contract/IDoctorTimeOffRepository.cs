using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract
{
    public interface IDoctorTimeOffRepository: IRepository<DoctorTimeOff, DoctorTimeOffId>
    {
        Task<IReadOnlyList<DoctorTimeOff>> GetByDoctorAsync(
            DoctorId doctorId,
            CancellationToken ct = default);

        Task<bool> HasConflictAsync(
            DoctorId doctorId,
            DateRange period,
            DoctorTimeOffId? excludeId = null,
            CancellationToken ct = default);
    }
}
