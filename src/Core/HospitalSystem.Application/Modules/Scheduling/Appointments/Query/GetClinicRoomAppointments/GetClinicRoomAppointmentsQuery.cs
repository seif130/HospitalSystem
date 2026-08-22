using HospitalSystem.Application.Modules.Scheduling.Appointments.Dto;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetClinicRoomAppointments
{
    public sealed record GetClinicRoomAppointmentsQuery(
        Guid ClinicRoomId,
        DateTime FromUtc,
        DateTime ToUtc)
        : IQuery<IReadOnlyList<AppointmentDto>>;

}
