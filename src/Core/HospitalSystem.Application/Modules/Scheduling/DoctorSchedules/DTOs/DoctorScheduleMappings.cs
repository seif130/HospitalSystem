using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.DTOs
{
    public static class DoctorScheduleMappings
    {
        public static DoctorScheduleDto ToDto(this DoctorSchedule schedule)
        {
            return new DoctorScheduleDto(
                schedule.Id.Value,
                schedule.DoctorId.Value,
                schedule.DayOfWeek,
                schedule.StartTime,
                schedule.EndTime);
        }
    }
}
