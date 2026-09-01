using HospitalSystem.Application.Modules.Procurement.Budget.DTOs;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Budget.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.Quires.GetBudgetsByDepartmentQuery
{
    public sealed class GetBudgetsByDepartmentQueryHandler
        : IQueryHandler<
            GetBudgetsByDepartmentQuery,
            IReadOnlyList<BudgetDto>>
    {
        private readonly IBudgetRepository _budgets;

        public GetBudgetsByDepartmentQueryHandler(
            IBudgetRepository budgets)
        {
            _budgets = budgets;
        }

        public async Task<Result<IReadOnlyList<BudgetDto>>> Handle(
            GetBudgetsByDepartmentQuery request,
            CancellationToken cancellationToken)
        {
            var budgets = await _budgets.GetByDepartmentAsync(
                new DepartmentId(request.DepartmentId),
                cancellationToken);

            return budgets
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
