using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.Batch
{
    public sealed record BatchRecalledDomainEvent(BatchId BatchId, MedicationId MedicationId, string LotNumber) : DomainEvent;

}
