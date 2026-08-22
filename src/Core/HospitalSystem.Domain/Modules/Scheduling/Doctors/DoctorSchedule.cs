using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors
{
    public sealed class DoctorSchedule: BaseEntity<DoctorScheduleId>
    {
        public DoctorId DoctorId { get; private set; } = null!;

        public DayOfWeek DayOfWeek { get; private set; }

        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        private DoctorSchedule()
        {
        }

        private DoctorSchedule(
            DoctorScheduleId id,
            DoctorId doctorId,
            DayOfWeek dayOfWeek,
            TimeSpan startTime,
            TimeSpan endTime)
            : base(id)
        {
            DoctorId = doctorId;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        public static DoctorSchedule Create(
            DoctorId doctorId,
            DayOfWeek dayOfWeek,
            TimeSpan startTime,
            TimeSpan endTime)
        {
            if (endTime <= startTime)
                throw new DomainException("Schedule end time must be after start time.");

            return new DoctorSchedule(DoctorScheduleId.New(),doctorId, dayOfWeek,startTime,endTime);
        }
    }

}
