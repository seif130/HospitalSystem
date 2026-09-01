using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Engagement.Complaint
{
    public sealed record CriticalComplaintRaisedDomainEvent(ComplaintId ComplaintId, PatientId PatientId, string Subject) : DomainEvent;

}
