using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors
{
    public sealed class DoctorAvailability
    {
        public DateRange Slot { get; }
        public bool IsBooked { get; private set; }

        internal DoctorAvailability(DateRange slot) => Slot = slot;

        internal void MarkBooked() => IsBooked = IsBooked
            ? throw new DomainException("This availability slot is already booked.")
            : true;

        internal void Release() => IsBooked = false;
    }
}
