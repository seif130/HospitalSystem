using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.InventoryItem
{
    public sealed record InventoryItemBelowReorderLevelDomainEvent(InventoryItemId InventoryItemId, int QuantityOnHand, int ReorderLevel) : DomainEvent;

}
