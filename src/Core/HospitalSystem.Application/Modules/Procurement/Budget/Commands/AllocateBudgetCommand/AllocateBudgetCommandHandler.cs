using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Domain.Modules.Procurement.Budget.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budget.Commands.AllocateBudgetCommand
{
    public sealed class AllocateBudgetCommandHandler
        : ICommandHandler<AllocateBudgetCommand, Guid>
    {
        private readonly IBudgetRepository _budgets;
        private readonly IDepartmentRepository _departments;
        private readonly IUnitOfWork _unitOfWork;

        public AllocateBudgetCommandHandler(
            IBudgetRepository budgets,
            IDepartmentRepository departments,
            IUnitOfWork unitOfWork)
        {
            _budgets = budgets;
            _departments = departments;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            AllocateBudgetCommand request,
            CancellationToken cancellationToken)
        {
            var departmentId =
                new DepartmentId(request.DepartmentId);

            var department = await _departments.GetByIdAsync(
                departmentId,
                cancellationToken);

            if (department is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "Department.NotFound",
                        "Department was not found."));
            }

            var fiscalPeriod = DateRange.Create(
                request.FromUtc,
                request.ToUtc);

            var exists = await _budgets.ExistsOverlappingPeriodAsync(
                departmentId,
                fiscalPeriod,
                cancellationToken);

            if (exists)
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "Budget.AlreadyExists",
                        "A budget already exists for the department during this fiscal period."));
            }

            var amount = Money.Create(
                request.Amount,
                request.Currency);

            var budget = Budget.Allocate(
                departmentId,
                fiscalPeriod,
                amount);

            await _budgets.AddAsync(
                budget,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success(
                budget.Id.Value);
        }
    }
}
