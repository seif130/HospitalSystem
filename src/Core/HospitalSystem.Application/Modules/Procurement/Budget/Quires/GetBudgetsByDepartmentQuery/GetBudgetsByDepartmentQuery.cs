using HospitalSystem.Application.Modules.Procurement.Budget.DTOs;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.Quires.GetBudgetsByDepartmentQuery
{
    public sealed record GetBudgetsByDepartmentQuery(
        Guid DepartmentId)
        : IQuery<IReadOnlyList<BudgetDto>>;
}
