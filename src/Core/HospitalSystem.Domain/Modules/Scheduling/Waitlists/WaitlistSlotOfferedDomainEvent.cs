using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Waitlists
{
    public sealed record WaitlistSlotOfferedDomainEvent(WaitlistId WaitlistId, PatientId PatientId, AppointmentId AppointmentId) : DomainEvent;
}
