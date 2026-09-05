using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Enum;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Events;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;

namespace HospitalSystem.Domain.Modules.Procurement.VendorContracts;

public sealed class VendorContract : AggregateRoot<VendorContractId>
{
    public VendorId VendorId { get; private set; }
    public VendorServiceCategory Category { get; private set; }
    public DateRange Term { get; private set; } = null!;
    public Money ContractValue { get; private set; } = null!;
    public VendorContractStatus Status { get; private set; }

    private VendorContract() { }

    private VendorContract(VendorContractId id, VendorId vendorId, VendorServiceCategory category, DateRange term, Money contractValue)
        : base(id)
    {
        VendorId = vendorId;
        Category = category;
        Term = term;
        ContractValue = contractValue;
        Status = VendorContractStatus.Draft;
    }

    public static VendorContract Draft(VendorId vendorId, VendorServiceCategory category, DateRange term, Money contractValue)
    {
        if (vendorId.IsEmpty) throw new DomainException("Vendor ID is required.");
        ArgumentNullException.ThrowIfNull(term);
        ArgumentNullException.ThrowIfNull(contractValue);

        if (term.IsOpen) throw new DomainException("A vendor contract must have a defined end date.");
        if (contractValue.Amount <= 0) throw new DomainException("Contract value must be greater than zero.");

        return new VendorContract(VendorContractId.New(), vendorId, category, term, contractValue);
    }

    public void Activate()
    {
        if (Status != VendorContractStatus.Draft)
            throw new DomainException("Only draft contracts can be activated.");
        Status = VendorContractStatus.Active;
    }

    public void ExpireIfPastEndDate(DateTime asOfUtc)
    {
        if (asOfUtc.Kind == DateTimeKind.Local)
            throw new DomainException("Contract expiration must use UTC or unspecified DateTime values.");
        if (Status != VendorContractStatus.Active || !Term.End.HasValue)
            return;
        if (asOfUtc >= Term.End.Value)
            Status = VendorContractStatus.Expired;
    }

    public void Terminate(string reason)
    {
        if (Status is VendorContractStatus.Expired or VendorContractStatus.Terminated)
            throw new DomainException($"Contract is already {Status}.");
        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Termination reason is required.");

        Status = VendorContractStatus.Terminated;
        AddDomainEvent(new VendorContractTerminatedDomainEvent(Id, reason.Trim()));
    }
}
