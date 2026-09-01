using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Surgeries
{
    public sealed record SurgeryCompletedDomainEvent(SurgeryId SurgeryId, PatientId PatientId) : DomainEvent;
}
