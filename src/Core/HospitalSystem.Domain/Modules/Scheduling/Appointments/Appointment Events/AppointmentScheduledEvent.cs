using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments.Appointment_Events
{
    public sealed record AppointmentScheduledEvent(
        AppointmentId AppointmentId,
        PatientId PatientId,
        DoctorId DoctorId,
        DateTime ScheduledAtUtc)
        : DomainEvent;

}
