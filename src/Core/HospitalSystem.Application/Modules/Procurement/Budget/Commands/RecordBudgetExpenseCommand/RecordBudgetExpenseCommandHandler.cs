using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.Commands.RecordBudgetExpenseCommand
{
    public sealed class RecordBudgetExpenseCommandHandler
        : ICommandHandler<RecordBudgetExpenseCommand>
    {
        private readonly IBudgetRepository _budgets;
        private readonly IUnitOfWork _unitOfWork;

        public RecordBudgetExpenseCommandHandler(
            IBudgetRepository budgets,
            IUnitOfWork unitOfWork)
        {
            _budgets = budgets;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            RecordBudgetExpenseCommand request,
            CancellationToken cancellationToken)
        {
            var budget = await _budgets.GetByIdAsync(
                new BudgetId(request.BudgetId),
                cancellationToken);

            if (budget is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Budget.NotFound",
                        "Budget was not found."));
            }

            var amount = Money.Create(
                request.Amount,
                request.Currency);

            budget.RecordExpense(
                request.Description,
                amount,
                request.IncurredOnUtc);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
