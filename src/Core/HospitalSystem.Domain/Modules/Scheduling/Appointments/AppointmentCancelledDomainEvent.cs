using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments
{
    public sealed record AppointmentCancelledDomainEvent(AppointmentId AppointmentId, PatientId PatientId, DoctorId DoctorId) : DomainEvent;

}
