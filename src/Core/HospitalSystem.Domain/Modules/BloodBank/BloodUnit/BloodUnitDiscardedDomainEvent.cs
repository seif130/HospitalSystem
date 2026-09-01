using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.BloodBank.BloodUnit
{
    public sealed record BloodUnitDiscardedDomainEvent(BloodUnitId BloodUnitId, string Reason) : DomainEvent;

}
