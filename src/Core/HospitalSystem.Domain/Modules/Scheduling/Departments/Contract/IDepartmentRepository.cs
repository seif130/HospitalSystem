using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Departments.Contract
{
    public interface IDepartmentRepository: IRepository<Department, DepartmentId>
    {
        Task<bool> ExistsByNameAsync(string name,CancellationToken ct = default);
        Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken ct = default);
        Task<Department?> GetByIdAsNoTrackingAsync(DepartmentId id,CancellationToken ct = default);
    }
}
