using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.LabResult
{
    public sealed record CriticalLabResultReportedDomainEvent(LabResultId LabResultId, LabOrderId LabOrderId, string TestCode) : DomainEvent;

}
