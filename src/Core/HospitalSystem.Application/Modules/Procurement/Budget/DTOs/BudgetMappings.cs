using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.DTOs
{
    public static class BudgetMappings
    {
        public static BudgetDto ToDto(this Budget budget)
        {
            return new BudgetDto(
                budget.Id.Value,
                budget.DepartmentId.Value,
                budget.FiscalPeriod.Start,
                budget.FiscalPeriod.End,
                budget.AllocatedAmount.Amount,
                budget.SpentAmount.Amount,
                budget.RemainingAmount.Amount,
                budget.AllocatedAmount.Currency,
                budget.Expenses
                    .Select(x => new BudgetExpenseLineDto(
                        x.Description,
                        x.Amount.Amount,
                        x.Amount.Currency,
                        x.IncurredOnUtc))
                    .ToList());
        }
    }
}
