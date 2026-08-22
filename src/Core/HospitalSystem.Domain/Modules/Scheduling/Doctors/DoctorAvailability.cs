using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors
{
    public sealed class DoctorAvailability: BaseEntity<DoctorAvailabilityId>
    {
        public DoctorId DoctorId { get; private set; } = null!;

        public DateRange Slot { get; private set; } = null!;

        private DoctorAvailability()
        {
        }

        private DoctorAvailability(
            DoctorAvailabilityId id,
            DoctorId doctorId,
            DateRange slot)
            : base(id)
        {
            DoctorId = doctorId;
            Slot = slot;
        }

        public static DoctorAvailability Create(
            DoctorId doctorId,
            DateRange slot)
        {
            ArgumentNullException.ThrowIfNull(slot);

            return new DoctorAvailability(
                DoctorAvailabilityId.New(),
                doctorId,
                slot);
        }

    }
}