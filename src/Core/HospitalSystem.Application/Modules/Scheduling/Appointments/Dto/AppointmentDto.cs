using HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Dto
{
    public sealed record AppointmentDto(
        Guid Id,
        Guid PatientId,
        Guid DoctorId,
        Guid ClinicRoomId,
        DateTime StartUtc,
        DateTime EndUtc,
        AppointmentType Type,
        AppointmentStatus Status,
        string? Reason,
        string? CancellationReason);

}
