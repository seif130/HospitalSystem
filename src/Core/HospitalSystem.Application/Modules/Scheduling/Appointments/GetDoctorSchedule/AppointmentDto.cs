using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule
{
    public sealed record AppointmentDto(
        Guid Id,
        Guid PatientId,
        Guid DoctorId,
        Guid ClinicRoomId,
        DateTime ScheduledAtUtc,
        string Status
    );
}
