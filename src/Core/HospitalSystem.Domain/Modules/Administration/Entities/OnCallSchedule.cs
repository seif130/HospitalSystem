using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
    public class OnCallSchedule : BaseEntity
    {
        public Guid DepartmentId { get; private set; }
        public Guid DoctorId { get; private set; }
        public DateTime DutyDate { get; private set; }
        public ShiftType Shift { get; private set; }

        public Department Department { get; private set; } = default!;

        private OnCallSchedule() { }

        private OnCallSchedule(Guid departmentId, Guid doctorId, DateTime dutyDate, ShiftType shift)
        {
            DepartmentId = departmentId;
            DoctorId = doctorId;
            DutyDate = dutyDate;
            Shift = shift;
        }

        internal static Result<OnCallSchedule> Create(Guid departmentId, Guid doctorId, DateTime dutyDate, ShiftType shift)
        {
            var errors = new List<Error>();

            if (departmentId == Guid.Empty)
                errors.Add(Error.Validation("Schedule.EmptyDepartmentId", "Department ID is required."));
            if (doctorId == Guid.Empty)
                errors.Add(Error.Validation("Schedule.EmptyDoctorId", "Doctor ID is required."));
            if (dutyDate.Date < DateTime.UtcNow.Date)
                errors.Add(Error.Validation("Schedule.InvalidDate", "Duty date cannot be in the past."));

            if (errors.Any())
                return Result<OnCallSchedule>.Fail(errors);

            return Result<OnCallSchedule>.Ok(new OnCallSchedule(departmentId, doctorId, dutyDate, shift));
        }
    }
}
