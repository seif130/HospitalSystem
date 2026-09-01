using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.BloodBank.BloodTransfusion
{
    public sealed record TransfusionAdverseReactionDomainEvent(BloodTransfusionId TransfusionId, PatientId PatientId, string Notes) : DomainEvent;

}
