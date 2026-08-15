using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.ScheduleAppointment
{
    public sealed record ScheduleAppointmentCommand(
        Guid PatientId,
        Guid DoctorId,
        Guid ClinicRoomId,
        DateTime ScheduledAtUtc,
        string? Reason
    ) : ICommand<Guid>;
}
