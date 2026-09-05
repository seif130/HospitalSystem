using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Events;

public sealed record PurchaseRequestApprovedDomainEvent(PurchaseRequestId PurchaseRequestId) : DomainEvent;
