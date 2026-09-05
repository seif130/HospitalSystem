using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand
{
    public sealed class RecordBudgetExpenseHandler(
        IBudgetRepository budgets,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RecordBudgetExpenseCommand>
    {
        public async Task<Result> Handle(
            RecordBudgetExpenseCommand request,
            CancellationToken cancellationToken)
        {
            var budget = await budgets.GetByIdAsync(
                request.BudgetId,
                cancellationToken);

            if (budget is null)
            {
                return Result.Failure(
                    Error.NotFound("Budget.NotFound",
                        "Budget was not found."));
            }

            budget.RecordExpense(
                request.Description,
                Money.Create(request.Amount,request.Currency),
                request.IncurredOnUtc);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
