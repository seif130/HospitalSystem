using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.LeaveRequest
{
    public sealed record LeaveRequestApprovedDomainEvent(LeaveRequestId LeaveRequestId, StaffId StaffId, DateRange Period) : DomainEvent;

}
