using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.Commands.RecordBudgetExpenseCommand
{
    public sealed record RecordBudgetExpenseCommand(
        Guid BudgetId,
        string Description,
        decimal Amount,
        string Currency,
        DateTime IncurredOnUtc)
        : ICommand;
}
