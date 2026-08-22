using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract
{
    public interface IWaitlistRepository: IRepository<Waitlist, WaitlistId>
    {
        Task<IReadOnlyList<Waitlist>> GetWaitingByDoctorAsync(DoctorId doctorId, DateRange period, CancellationToken ct = default);

        Task<bool> HasActiveEntryAsync(PatientId patientId, DoctorId doctorId,CancellationToken ct = default);
    }

}
