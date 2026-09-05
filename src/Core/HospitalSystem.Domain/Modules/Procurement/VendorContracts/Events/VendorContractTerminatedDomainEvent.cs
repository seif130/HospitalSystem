using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;

public sealed record VendorContractTerminatedDomainEvent(VendorContractId VendorContractId, string Reason) : DomainEvent;
