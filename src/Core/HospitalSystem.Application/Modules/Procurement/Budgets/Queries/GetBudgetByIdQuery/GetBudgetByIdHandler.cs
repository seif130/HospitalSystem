using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budgets.Queries.GetBudgetByIdQuery
{
    public sealed class GetBudgetByIdHandler(
        IBudgetRepository budgets)
        : IQueryHandler<GetBudgetByIdQuery, BudgetDto>
    {
        public async Task<Result<BudgetDto>> Handle(
            GetBudgetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var budget = await budgets.GetByIdAsync(
                request.BudgetId,
                cancellationToken);

            if (budget is null)
            {
                return Result.Failure<BudgetDto>(
                    Error.NotFound(
                        "Budget.NotFound",
                        "Budget was not found."));
            }

            return Result.Success(
                budget.ToDto());
        }
    }
}
