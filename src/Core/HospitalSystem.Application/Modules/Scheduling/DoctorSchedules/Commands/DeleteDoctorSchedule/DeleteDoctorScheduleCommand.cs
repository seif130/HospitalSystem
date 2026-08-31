using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.Commands.DeleteDoctorSchedule
{
    public sealed record DeleteDoctorScheduleCommand(
        Guid DoctorScheduleId)
        : ICommand;
}
