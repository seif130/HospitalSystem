using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments
{
    public sealed record AppointmentScheduledDomainEvent(AppointmentId AppointmentId, PatientId PatientId, DoctorId DoctorId, DateTime ScheduledAtUtc) : DomainEvent;
}
