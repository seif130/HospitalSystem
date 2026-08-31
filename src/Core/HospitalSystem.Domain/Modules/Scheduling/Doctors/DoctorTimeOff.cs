using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors
{
    public sealed class DoctorTimeOff: AggregateRoot<DoctorTimeOffId>
    {
        public DoctorId DoctorId { get; private set; } = null!;

        public DateRange Period { get; private set; } = null!;

        public string? Reason { get; private set; }

        private DoctorTimeOff()
        {
        }

        private DoctorTimeOff(DoctorTimeOffId id,
            DoctorId doctorId,DateRange period,
            string? reason): base(id)
        {
            DoctorId = doctorId;
            Period = period;
            Reason = NormalizeOptional(reason);
        }

        public static DoctorTimeOff Create(
            DoctorId doctorId,
            DateRange period,
            string? reason = null)
        {
            ArgumentNullException.ThrowIfNull(period);

            return new DoctorTimeOff(
                DoctorTimeOffId.New(),
                doctorId,
                period,
                reason);
        }

        public void UpdateReason(string? reason)
        {
            Reason = NormalizeOptional(reason);
        }

        public void UpdatePeriod(DateRange period)
        {
            ArgumentNullException.ThrowIfNull(period);

            Period = period;
        }

        private static string? NormalizeOptional(string? value)
        {
            return string.IsNullOrWhiteSpace(value)? null: value.Trim();
        }
    }
}