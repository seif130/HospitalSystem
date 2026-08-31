using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.Commands.UpdateDoctorSchedule
{
    public sealed record UpdateDoctorScheduleCommand(
        Guid DoctorScheduleId,
        DayOfWeek DayOfWeek,
        TimeSpan StartTime,
        TimeSpan EndTime)
        : ICommand;
}
