using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.PurchaseOrder
{
    public sealed record PurchaseOrderSubmittedDomainEvent(PurchaseOrderId PurchaseOrderId, SupplierId SupplierId, Money Total) : DomainEvent;

}
