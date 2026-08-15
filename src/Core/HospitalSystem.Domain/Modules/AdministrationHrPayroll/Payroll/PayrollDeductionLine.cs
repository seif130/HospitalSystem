using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.Payroll
{
    public sealed record PayrollDeductionLine(string Reason, Money Amount);

}
