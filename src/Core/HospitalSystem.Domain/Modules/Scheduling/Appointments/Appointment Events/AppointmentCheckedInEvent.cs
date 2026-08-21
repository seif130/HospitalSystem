using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments.Appointment_Events
{
    public sealed record AppointmentCheckedInEvent(
        AppointmentId AppointmentId,
        PatientId PatientId,
        DoctorId DoctorId) : DomainEvent;

}
