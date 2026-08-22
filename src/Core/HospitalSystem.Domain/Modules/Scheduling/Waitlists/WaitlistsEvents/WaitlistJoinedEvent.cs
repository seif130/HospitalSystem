using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Waitlists.WaitlistsEvents
{
    public sealed record WaitlistJoinedEvent(
        WaitlistId WaitlistId,
        PatientId PatientId,
        DoctorId DoctorId,
        DateTime PreferredFromUtc,
        DateTime PreferredToUtc
    ) : DomainEvent;

}
