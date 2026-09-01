using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Procurement.Budgets.Contract
{
    public interface IBudgetRepository: IRepository<Budgets,BudgetId>
    {
        Task<bool> ExistsOverlappingPeriodAsync(
            DepartmentId departmentId,DateRange fiscalPeriod,
            CancellationToken ct = default);

        Task<IReadOnlyList<Budgets>> GetByDepartmentAsync(
            DepartmentId departmentId, CancellationToken ct = default);
    }
}
