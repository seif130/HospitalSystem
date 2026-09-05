using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand
{
    public sealed record RecordBudgetExpenseCommand(BudgetId BudgetId, string Description, decimal Amount, string Currency, DateTime IncurredOnUtc) : ICommand;

}
