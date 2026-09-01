using HospitalSystem.Application.Modules.Procurement.Budget.DTOs;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.Quires.GetBudgetByIdQuery
{
    public sealed class GetBudgetByIdQueryHandler
        : IQueryHandler<GetBudgetByIdQuery, BudgetDto>
    {
        private readonly IBudgetRepository _budgets;

        public GetBudgetByIdQueryHandler(
            IBudgetRepository budgets)
        {
            _budgets = budgets;
        }

        public async Task<Result<BudgetDto>> Handle(
            GetBudgetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var budget = await _budgets.GetByIdAsync(new BudgetId(request.BudgetId),
                cancellationToken);

            if (budget is null)
            {
                return Result.Failure<BudgetDto>(
                    Error.NotFound(
                        "Budget.NotFound",
                        "Budget was not found."));
            }

            return budget.ToDto();
        }
    }
}
