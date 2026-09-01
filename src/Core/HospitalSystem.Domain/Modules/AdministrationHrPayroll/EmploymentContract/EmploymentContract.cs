using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.EmploymentContract.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.EmploymentContract
{
    public sealed class EmploymentContract : AggregateRoot<EmploymentContractId>
    {
        public StaffId StaffId { get; private set; } = null!;
        public ContractType Type { get; private set; }
        public DateRange Term { get; private set; } = null!;
        public Money AgreedSalary { get; private set; } = null!;
        public bool IsSigned { get; private set; }

        private EmploymentContract() { }

        private EmploymentContract(EmploymentContractId id, StaffId staffId, ContractType type, DateRange term, Money agreedSalary) : base(id)
        {
            StaffId = staffId;
            Type = type;
            Term = term;
            AgreedSalary = agreedSalary;
        }

        public static EmploymentContract Draft(StaffId staffId, ContractType type, DateRange term, Money agreedSalary) =>
            new(EmploymentContractId.New(), staffId, type, term, agreedSalary);

        public void Sign()
        {
            if (IsSigned) throw new DomainException("Contract is already signed.");
            IsSigned = true;
            AddDomainEvent(new EmploymentContractSignedDomainEvent(Id, StaffId));
        }

        public bool IsExpired(DateTime asOfUtc) => Term.End.HasValue && asOfUtc > Term.End.Value;
    }
}
