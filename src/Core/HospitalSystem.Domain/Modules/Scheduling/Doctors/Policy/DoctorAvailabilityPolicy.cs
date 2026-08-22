using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors.Policy
{
    public sealed class DoctorAvailabilityPolicy
    {
        public bool IsAvailable( DateRange requestedPeriod, IEnumerable<DoctorSchedule> schedules,
            IEnumerable<DoctorTimeOff> timeOffs)
        {
            var end = requestedPeriod.End;

            if (end is null)
                return false;

            var dayOfWeek = requestedPeriod.Start.DayOfWeek;

            var startTime = requestedPeriod.Start.TimeOfDay;
            var endTime = end.Value.TimeOfDay;

            var isWithinWorkingSchedule = schedules.Any(schedule =>
                schedule.DayOfWeek == dayOfWeek &&
                schedule.StartTime <= startTime &&
                schedule.EndTime >= endTime);

            if (!isWithinWorkingSchedule)
                return false;

            var isOnTimeOff = timeOffs.Any(timeOff =>
                timeOff.Period.Overlaps(requestedPeriod));

            return !isOnTimeOff;
        }
    }

}
