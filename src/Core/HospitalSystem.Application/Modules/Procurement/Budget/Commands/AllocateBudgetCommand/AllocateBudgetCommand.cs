using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.Commands.AllocateBudgetCommand
{
    public sealed record AllocateBudgetCommand(
        Guid DepartmentId,
        DateTime FromUtc,
        DateTime ToUtc,
        decimal Amount,
        string Currency)
        : ICommand<Guid>;
}
