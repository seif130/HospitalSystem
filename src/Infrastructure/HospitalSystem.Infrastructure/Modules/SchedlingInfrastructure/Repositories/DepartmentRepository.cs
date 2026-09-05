using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Departments;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.SchedlingInfrastructure.Repositories
{
    public sealed class DepartmentRepository: Repository<Department, DepartmentId>,IDepartmentRepository
    {
        public DepartmentRepository(SchedulingDbContext context): base(context)
        {
        }

        public Task<bool> ExistsByNameAsync(string name,CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult(false);

            var normalized = name.Trim();

            return DbSet.AnyAsync(x => x.Name == normalized, ct);
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync(
            CancellationToken ct = default)
        {
            return await DbSet.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);
        }

        public async Task<Department?> GetByIdAsNoTrackingAsync(DepartmentId id,CancellationToken ct = default)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        }
    }



}
