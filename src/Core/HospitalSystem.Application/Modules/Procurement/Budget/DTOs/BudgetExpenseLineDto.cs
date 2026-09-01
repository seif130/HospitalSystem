using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.DTOs
{
    public sealed record BudgetExpenseLineDto(
        string Description,
        decimal Amount,
        string Currency,
        DateTime IncurredOnUtc);
}
