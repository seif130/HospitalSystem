using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Procurement.Budgets.Contract
{
    public interface IBudgetRepository: IRepository<Budget,BudgetId>
    {
        Task<(IReadOnlyList<Budget> Items, int TotalCount)> GetByDepartmentAsync(
         DepartmentId departmentId,
         int pageNumber,
         int pageSize,
         CancellationToken ct = default);

        Task<bool> ExistsOverlappingAsync(
            DepartmentId departmentId,
            DateRange fiscalPeriod,
            CancellationToken ct = default);
    }
}
