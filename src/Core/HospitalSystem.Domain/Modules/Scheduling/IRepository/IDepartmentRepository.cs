using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.IRepository
{

    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default);
        void Add(Department department);
    }
}
