using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Dto
{
    public sealed record WaitlistDto(
        Guid Id,
        Guid PatientId,
        Guid DoctorId,
        DateTime PreferredFromUtc,
        DateTime PreferredToUtc,
        DateTime JoinedOnUtc,
        WaitlistEntryStatus Status);

}
