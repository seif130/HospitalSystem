using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.ShiftSchedule.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.ShiftSchedule
{
    public sealed class ShiftSchedule : AggregateRoot<ShiftScheduleId>
    {
        public StaffId StaffId { get; private set; } = null!;
        public DateRange Shift { get; private set; } = null!;
        public ShiftType Type { get; private set; }
        public bool IsCancelled { get; private set; }

        private ShiftSchedule() { }

        private ShiftSchedule(ShiftScheduleId id, StaffId staffId, DateRange shift, ShiftType type) : base(id)
        {
            StaffId = staffId;
            Shift = shift;
            Type = type;
        }

        public static ShiftSchedule Plan(StaffId staffId, DateRange shift, ShiftType type)
        {
            if (shift.IsOpen) throw new DomainException("A planned shift must have a defined end time.");
            return new ShiftSchedule(ShiftScheduleId.New(), staffId, shift, type);
        }

        public void Cancel()
        {
            if (IsCancelled) throw new DomainException("Shift is already cancelled.");
            IsCancelled = true;
        }
    }
}
