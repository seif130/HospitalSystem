using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Enum;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Events;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;

public sealed class PurchaseRequest : AggregateRoot<PurchaseRequestId>
{
    public DepartmentId DepartmentId { get; private set; }
    public string Reason { get; private set; } = null!;
    public PurchaseRequestStatus Status { get; private set; }

    private readonly List<PurchaseRequestLine> _lines = [];
    public IReadOnlyCollection<PurchaseRequestLine> Lines => _lines.AsReadOnly();

    private PurchaseRequest() { }

    private PurchaseRequest(PurchaseRequestId id, DepartmentId departmentId, string reason) : base(id)
    {
        DepartmentId = departmentId;
        Reason = reason;
        Status = PurchaseRequestStatus.Draft;
    }

    public static PurchaseRequest Create(DepartmentId departmentId, string reason)
    {
        if (departmentId.IsEmpty) throw new DomainException("Department ID is required.");
        if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Purchase request reason is required.");
        return new PurchaseRequest(PurchaseRequestId.New(), departmentId, reason.Trim());
    }

    public void AddLine(string itemName, int quantity, Money estimatedUnitPrice)
    {
        EnsureStatus(PurchaseRequestStatus.Draft, "Only draft purchase requests can be modified.");
        _lines.Add(new PurchaseRequestLine(itemName, quantity, estimatedUnitPrice));
    }

    public void Submit()
    {
        EnsureStatus(PurchaseRequestStatus.Draft, "Only draft purchase requests can be submitted.");
        if (_lines.Count == 0) throw new DomainException("Purchase request must contain at least one line.");
        Status = PurchaseRequestStatus.Submitted;
    }

    public void Approve()
    {
        EnsureStatus(PurchaseRequestStatus.Submitted, "Only submitted purchase requests can be approved.");
        Status = PurchaseRequestStatus.Approved;
        AddDomainEvent(new PurchaseRequestApprovedDomainEvent(Id));
    }

    public void Reject(string reason)
    {
        EnsureStatus(PurchaseRequestStatus.Submitted, "Only submitted purchase requests can be rejected.");
        if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Rejection reason is required.");
        Status = PurchaseRequestStatus.Rejected;
        AddDomainEvent(new PurchaseRequestRejectedDomainEvent(Id, reason.Trim()));
    }

    public void Cancel()
    {
        if (Status is PurchaseRequestStatus.Approved or PurchaseRequestStatus.Rejected or PurchaseRequestStatus.Cancelled)
            throw new DomainException("Purchase request cannot be cancelled.");
        Status = PurchaseRequestStatus.Cancelled;
    }

    private void EnsureStatus(PurchaseRequestStatus expected, string message)
    {
        if (Status != expected) throw new DomainException(message);
    }
}
