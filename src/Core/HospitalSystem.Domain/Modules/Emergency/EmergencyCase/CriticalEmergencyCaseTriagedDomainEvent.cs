using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Emergency.TriageRecord.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Emergency.EmergencyCase
{
    public sealed record CriticalEmergencyCaseTriagedDomainEvent(EmergencyCaseId EmergencyCaseId, PatientId PatientId, TriageLevel Level) : DomainEvent;

}
