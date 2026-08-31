using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.Commands.CreateDoctorSchedule
{
    public sealed record CreateDoctorScheduleCommand(
        Guid DoctorId,
        DayOfWeek DayOfWeek,
        TimeSpan StartTime,
        TimeSpan EndTime)
        : ICommand<Guid>;
}
