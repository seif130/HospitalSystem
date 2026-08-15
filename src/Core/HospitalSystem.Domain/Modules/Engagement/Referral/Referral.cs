using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Engagement.Referral.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Engagement.Referral
{
    public sealed class Referral : AggregateRoot<ReferralId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId ReferringDoctorId { get; private set; } = null!;
        public string ReferredToName { get; private set; } = null!; // external doctor/facility name, or DoctorId as string if internal
        public ReferralDirection Direction { get; private set; }
        public string Reason { get; private set; } = null!;
        public ReferralStatus Status { get; private set; }

        private Referral() { }

        private Referral(ReferralId id, PatientId patientId, DoctorId referringDoctorId, string referredToName,
            ReferralDirection direction, string reason) : base(id)
        {
            PatientId = patientId;
            ReferringDoctorId = referringDoctorId;
            ReferredToName = referredToName;
            Direction = direction;
            Reason = reason;
            Status = ReferralStatus.Pending;
        }

        public static Referral Create(PatientId patientId, DoctorId referringDoctorId, string referredToName, ReferralDirection direction, string reason)
        {
            if (string.IsNullOrWhiteSpace(referredToName)) throw new DomainException("Referred-to name is required.");
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Referral reason is required.");
            return new Referral(ReferralId.New(), patientId, referringDoctorId, referredToName.Trim(), direction, reason.Trim());
        }

        public void Accept()
        {
            if (Status != ReferralStatus.Pending) throw new DomainException("Only a pending referral can be accepted.");
            Status = ReferralStatus.Accepted;
        }

        public void Complete()
        {
            if (Status != ReferralStatus.Accepted) throw new DomainException("Referral must be accepted before completion.");
            Status = ReferralStatus.Completed;
        }

        public void Decline(string reason)
        {
            if (Status != ReferralStatus.Pending) throw new DomainException("Only a pending referral can be declined.");
            Status = ReferralStatus.Declined;
            Reason = reason.Trim();
        }
    }
}
