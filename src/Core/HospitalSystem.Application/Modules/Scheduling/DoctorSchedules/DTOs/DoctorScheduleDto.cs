using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.DTOs
{
    public sealed record DoctorScheduleDto(
        Guid Id,
        Guid DoctorId,
        DayOfWeek DayOfWeek,
        TimeSpan StartTime,
        TimeSpan EndTime);
}
