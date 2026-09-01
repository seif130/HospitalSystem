using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.EmploymentContract
{
    public sealed record EmploymentContractSignedDomainEvent(EmploymentContractId ContractId, StaffId StaffId) : DomainEvent;

}
