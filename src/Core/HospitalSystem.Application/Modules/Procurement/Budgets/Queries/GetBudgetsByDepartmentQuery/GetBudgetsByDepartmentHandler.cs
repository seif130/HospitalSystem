using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budgets.Queries.GetBudgetsByDepartmentQuery
{
    public sealed class GetBudgetsByDepartmentHandler(
        IBudgetRepository budgets)
        : IQueryHandler<GetBudgetsByDepartmentQuery, PaginatedList<BudgetDto>>
    {
        public async Task<Result<PaginatedList<BudgetDto>>> Handle(
            GetBudgetsByDepartmentQuery request,
            CancellationToken cancellationToken)
        {
            var (budgetsList, total) = await budgets.GetByDepartmentAsync(
                request.DepartmentId,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var items = budgetsList
                .Select(budget => budget.ToDto())
                .ToList();

            var result = new PaginatedList<BudgetDto>(
                items,
                total,
                request.PageNumber,
                request.PageSize);

            return Result.Success(result);
        }
    }
}
