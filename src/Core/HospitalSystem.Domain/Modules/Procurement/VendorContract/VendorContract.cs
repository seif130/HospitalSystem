using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Procurement.VendorContract.Enums;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Procurement.VendorContract
{
    public sealed class VendorContract : AggregateRoot<VendorContractId>
    {
        public string VendorName { get; private set; } = null!;
        public VendorServiceCategory Category { get; private set; }
        public DateRange Term { get; private set; } = null!;
        public Money ContractValue { get; private set; } = null!;
        public VendorContractStatus Status { get; private set; }

        private VendorContract() { }

        private VendorContract(VendorContractId id, string vendorName, VendorServiceCategory category, DateRange term, Money contractValue) : base(id)
        {
            VendorName = vendorName;
            Category = category;
            Term = term;
            ContractValue = contractValue;
            Status = VendorContractStatus.Draft;
        }

        public static VendorContract Draft(string vendorName, VendorServiceCategory category, DateRange term, Money contractValue)
        {
            if (string.IsNullOrWhiteSpace(vendorName)) throw new DomainException("Vendor name is required.");
            if (term.IsOpen) throw new DomainException("A vendor contract must have a defined end date.");
            return new VendorContract(VendorContractId.New(), vendorName.Trim(), category, term, contractValue);
        }

        public void Activate()
        {
            if (Status != VendorContractStatus.Draft) throw new DomainException("Only a draft contract can be activated.");
            Status = VendorContractStatus.Active;
        }

        public void ExpireIfPastEndDate(DateTime asOfUtc)
        {
            if (Status == VendorContractStatus.Active && Term.End.HasValue && asOfUtc > Term.End.Value)
                Status = VendorContractStatus.Expired;
        }

        public void Terminate(string reason)
        {
            if (Status is VendorContractStatus.Expired or VendorContractStatus.Terminated)
                throw new DomainException($"Contract is already {Status}.");
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Termination reason is required.");
            Status = VendorContractStatus.Terminated;
            RaiseDomainEvent(new VendorContractTerminatedDomainEvent(Id, reason));
        }
    }
}
