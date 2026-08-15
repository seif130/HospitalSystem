using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.Payroll
{
    public sealed record PayrollPaidDomainEvent(PayrollId PayrollId, StaffId StaffId, Money NetAmount) : DomainEvent;

}
