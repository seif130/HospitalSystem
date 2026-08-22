using HospitalSystem.Application.Modules.Scheduling.Appointments.Dto;
using HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetPatientAppointments
{
    public sealed record GetPatientAppointmentsQuery(
        Guid PatientId,
        DateTime FromUtc,
        DateTime ToUtc) : IQuery<IReadOnlyList<AppointmentDto>>;
}
