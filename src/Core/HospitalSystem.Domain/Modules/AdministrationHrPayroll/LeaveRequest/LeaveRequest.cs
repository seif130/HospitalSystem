using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.LeaveRequest.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.LeaveRequest
{
    public sealed class LeaveRequest : AggregateRoot<LeaveRequestId>
    {
        public StaffId StaffId { get; private set; } = null!;
        public LeaveType Type { get; private set; }
        public DateRange Period { get; private set; } = null!;
        public string? Reason { get; private set; }
        public LeaveRequestStatus Status { get; private set; }
        public string? ReviewedByStaffId { get; private set; }

        private LeaveRequest() { }

        private LeaveRequest(LeaveRequestId id, StaffId staffId, LeaveType type, DateRange period, string? reason) : base(id)
        {
            StaffId = staffId;
            Type = type;
            Period = period;
            Reason = reason;
            Status = LeaveRequestStatus.Pending;
        }

        public static LeaveRequest Submit(StaffId staffId, LeaveType type, DateRange period, string? reason = null)
        {
            if (period.IsOpen) throw new DomainException("A leave request must have a defined end date.");
            return new LeaveRequest(LeaveRequestId.New(), staffId, type, period, reason?.Trim());
        }

        public void Approve(string reviewedByStaffId)
        {
            if (Status != LeaveRequestStatus.Pending) throw new DomainException("Only a pending leave request can be approved.");
            Status = LeaveRequestStatus.Approved;
            ReviewedByStaffId = reviewedByStaffId;
            AddDomainEvent(new LeaveRequestApprovedDomainEvent(Id, StaffId, Period));
        }

        public void Reject(string reviewedByStaffId, string reason)
        {
            if (Status != LeaveRequestStatus.Pending) throw new DomainException("Only a pending leave request can be rejected.");
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Rejection reason is required.");
            Status = LeaveRequestStatus.Rejected;
            ReviewedByStaffId = reviewedByStaffId;
            Reason = reason.Trim();
        }

        public void Cancel()
        {
            if (Status == LeaveRequestStatus.Approved) throw new DomainException("Cannot cancel an already-approved leave request; withdraw it instead.");
            Status = LeaveRequestStatus.Cancelled;
        }
    }
}
