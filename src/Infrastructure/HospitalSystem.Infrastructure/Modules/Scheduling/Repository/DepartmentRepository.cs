using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Departments;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Scheduling.Repository
{
    internal sealed class DepartmentRepository : IDepartmentRepository
    {
        private readonly SchedulingDbContext _context;

        public DepartmentRepository(SchedulingDbContext context) => _context = context;

        public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var departmentId = new DepartmentId(id);
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);
        }

        public async Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Departments.ToListAsync(cancellationToken);
        }

        public void Add(Department department)
        {
            _context.Departments.Add(department);
        }
    }
}
