using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.Specimen
{
    public sealed record SpecimenCollectedDomainEvent(SpecimenId SpecimenId, LabOrderId LabOrderId) : DomainEvent;

}
