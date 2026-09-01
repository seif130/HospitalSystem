using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Procurement.Budget.Contract
{
    public interface IBudgetRepository: IRepository<Budget, BudgetId>
    {
        Task<bool> ExistsOverlappingPeriodAsync(
            DepartmentId departmentId,DateRange fiscalPeriod,
            CancellationToken ct = default);

        Task<IReadOnlyList<Budget>> GetByDepartmentAsync(
            DepartmentId departmentId, CancellationToken ct = default);
    }
}
