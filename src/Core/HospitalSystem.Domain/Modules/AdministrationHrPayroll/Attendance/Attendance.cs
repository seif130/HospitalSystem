using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.Attendance.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.Attendance
{

    public sealed class Attendance : AggregateRoot<AttendanceId>
    {
        public StaffId StaffId { get; private set; } = null!;
        public DateTime Date { get; private set; }
        public DateTime? ClockInUtc { get; private set; }
        public DateTime? ClockOutUtc { get; private set; }
        public AttendanceStatus Status { get; private set; }

        private Attendance() { }

        private Attendance(AttendanceId id, StaffId staffId, DateTime date) : base(id)
        {
            StaffId = staffId;
            Date = date.Date;
        }

        public static Attendance StartDay(StaffId staffId, DateTime date) => new(AttendanceId.New(), staffId, date);

        public void ClockIn(DateTime timeUtc, TimeSpan shiftStart)
        {
            if (ClockInUtc.HasValue) throw new DomainException("Already clocked in for this day.");
            ClockInUtc = timeUtc;
            Status = timeUtc.TimeOfDay > shiftStart ? AttendanceStatus.Late : AttendanceStatus.Present;
        }

        public void ClockOut(DateTime timeUtc)
        {
            if (!ClockInUtc.HasValue) throw new DomainException("Cannot clock out before clocking in.");
            if (timeUtc < ClockInUtc.Value) throw new DomainException("Clock-out time cannot be before clock-in time.");
            ClockOutUtc = timeUtc;
        }

        public void MarkAbsent()
        {
            if (ClockInUtc.HasValue) throw new DomainException("Cannot mark absent after clocking in.");
            Status = AttendanceStatus.Absent;
        }
    }
}
