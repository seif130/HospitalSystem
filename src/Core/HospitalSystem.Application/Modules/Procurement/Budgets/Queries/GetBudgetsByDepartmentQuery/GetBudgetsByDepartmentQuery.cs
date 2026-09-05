using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budgets.Queries.GetBudgetsByDepartmentQuery
{
    public sealed record GetBudgetsByDepartmentQuery(DepartmentId DepartmentId, int PageNumber = 1, int PageSize = 20) : IQuery<PaginatedList<BudgetDto>>;

}
