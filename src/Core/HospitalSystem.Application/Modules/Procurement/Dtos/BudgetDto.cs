using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Dtos
{
    public sealed record BudgetDto(
        Guid Id, Guid DepartmentId, DateTime FiscalStart,
        DateTime FiscalEnd, decimal AllocatedAmount, decimal SpentAmount,
        decimal RemainingAmount, string Currency);

}
