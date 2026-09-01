using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.DTOs
{
    public sealed record BudgetDto(
        Guid Id,
        Guid DepartmentId,
        DateTime StartUtc,
        DateTime? EndUtc,
        decimal AllocatedAmount,
        decimal SpentAmount,
        decimal RemainingAmount,
        string Currency,
        IReadOnlyList<BudgetExpenseLineDto> Expenses);
}
