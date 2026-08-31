using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.Commands.CreateDoctorTimeOff
{
    public sealed record CreateDoctorTimeOffCommand(
        Guid DoctorId,
        DateTime FromUtc,
        DateTime? ToUtc,
        string? Reason)
        : ICommand<Guid>;
}
