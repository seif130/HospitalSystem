using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budgets.Commands.AllocateBudgetCommand
{
    public sealed record AllocateBudgetCommand(DepartmentId DepartmentId, DateTime FiscalStart, DateTime FiscalEnd, decimal Amount, string Currency) : ICommand<BudgetId>;

}
