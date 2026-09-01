using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Procurement.VendorContract
{
    public sealed record VendorContractTerminatedDomainEvent(VendorContractId ContractId, string Reason) : DomainEvent;

}
