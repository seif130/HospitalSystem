using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.ScheduleAppointment
{
    public sealed record ScheduleAppointmentCommand(
        Guid PatientId,
        Guid DoctorId,
        Guid ClinicRoomId,
        DateTime StartUtc,
        DateTime EndUtc,
        AppointmentType Type,
        string? Reason) : ICommand<Guid>;

}
