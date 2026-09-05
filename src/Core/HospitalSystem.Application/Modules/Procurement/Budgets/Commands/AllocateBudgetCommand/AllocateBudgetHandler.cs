using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budgets.Commands.AllocateBudgetCommand
{
    public sealed class AllocateBudgetHandler(IBudgetRepository budgets,IUnitOfWork unitOfWork)
    : ICommandHandler<AllocateBudgetCommand, BudgetId>
    {
        public async Task<Result<BudgetId>> Handle(
            AllocateBudgetCommand request,
            CancellationToken cancellationToken)
        {
            var period = DateRange.Create(request.FiscalStart,request.FiscalEnd);

            var exists = await budgets.ExistsOverlappingAsync(
                request.DepartmentId,period,cancellationToken);

            if (exists)
            {
                return Result.Failure<BudgetId>(Error.Conflict("Budget.Overlap",
                        "An overlapping budget already exists for this department."));
            }

            var budget = Budget.Allocate(
                request.DepartmentId,
                period,
                Money.Create(request.Amount,request.Currency));

            await budgets.AddAsync(budget,cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(budget.Id);
        }
    }
}
