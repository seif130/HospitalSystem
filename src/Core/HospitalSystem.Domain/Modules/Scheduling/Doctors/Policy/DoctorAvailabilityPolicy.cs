using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors.Policy
{
    public sealed class DoctorAvailabilityPolicy
    {
        public bool IsAvailable(DateRange requestedPeriod,IEnumerable<DoctorSchedule> schedules,
            IEnumerable<DoctorTimeOff> timeOffs)
        {
            ArgumentNullException.ThrowIfNull(requestedPeriod);
            ArgumentNullException.ThrowIfNull(schedules);
            ArgumentNullException.ThrowIfNull(timeOffs);

            if (requestedPeriod.End is null)
                return false;

            var end = requestedPeriod.End.Value;

            var dayOfWeek = requestedPeriod.Start.DayOfWeek;

            var startTime = requestedPeriod.Start.TimeOfDay;
            var endTime = end.TimeOfDay;

            var isWithinSchedule = schedules.Any(schedule =>
                schedule.DayOfWeek == dayOfWeek &&
                schedule.StartTime <= startTime &&
                schedule.EndTime >= endTime);

            if (!isWithinSchedule)
                return false;

            var isTimeOff = timeOffs.Any(timeOff =>
                timeOff.Period.Overlaps(requestedPeriod));

            return !isTimeOff;
        }
    }

}
