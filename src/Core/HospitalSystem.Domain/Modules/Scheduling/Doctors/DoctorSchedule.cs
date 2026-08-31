using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors
{
    public sealed class DoctorSchedule: AggregateRoot<DoctorScheduleId>
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
            ValidateTime(startTime, endTime);

            return new DoctorSchedule(
                DoctorScheduleId.New(),
                doctorId,
                dayOfWeek,
                startTime,
                endTime);
        }

        public void Update(
            DayOfWeek dayOfWeek,
            TimeSpan startTime,
            TimeSpan endTime)
        {
            ValidateTime(startTime, endTime);

            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        private static void ValidateTime(
            TimeSpan startTime,
            TimeSpan endTime)
        {
            if (startTime < TimeSpan.Zero)
            {
                throw new DomainException(
                    "Schedule start time cannot be negative.");
            }

            if (endTime <= startTime)
            {
                throw new DomainException(
                    "Schedule end time must be after start time.");
            }

            if (endTime > TimeSpan.FromDays(1))
            {
                throw new DomainException(
                    "Schedule end time cannot exceed 24 hours.");
            }
        }
    }

}
