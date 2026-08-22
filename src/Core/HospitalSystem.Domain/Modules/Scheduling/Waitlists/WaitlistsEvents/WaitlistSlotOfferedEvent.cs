using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Waitlists.WaitlistsEvents
{
    public sealed record WaitlistSlotOfferedEvent(
        WaitlistId WaitlistId,
        PatientId PatientId,
        AppointmentId AppointmentId
    ) : DomainEvent;

}
