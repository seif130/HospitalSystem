using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.Commands.DeleteDoctorTimeOff
{
    public sealed record DeleteDoctorTimeOffCommand(
        Guid DoctorTimeOffId)
        : ICommand;
}
