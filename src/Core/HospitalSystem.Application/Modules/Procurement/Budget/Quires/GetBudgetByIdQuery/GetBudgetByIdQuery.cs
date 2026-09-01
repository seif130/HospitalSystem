using HospitalSystem.Application.Modules.Procurement.Budget.DTOs;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.Quires.GetBudgetByIdQuery
{
    public sealed record GetBudgetByIdQuery(
        Guid BudgetId)
        : IQuery<BudgetDto>;
}
