using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.Commands.UpdateDoctorTimeOff
{
    public sealed record UpdateDoctorTimeOffCommand(
        Guid DoctorTimeOffId,
        DateTime FromUtc,
        DateTime? ToUtc,
        string? Reason)
        : ICommand;
}
