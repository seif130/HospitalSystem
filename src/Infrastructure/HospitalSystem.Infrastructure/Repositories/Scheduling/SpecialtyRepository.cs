using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Specialties;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Repositories.Scheduling
{
    public sealed class SpecialtyRepository
        : Repository<Specialty, SpecialtyId>,ISpecialtyRepository
    {
        public SpecialtyRepository(SchedulingDbContext context): base(context)
        {
        }

        public Task<bool> ExistsByNameAsync(
            string name,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult(false);

            var normalized = name.Trim();

            return DbSet.AnyAsync(
                x => x.Name == normalized,
                ct);
        }

        public async Task<IReadOnlyList<Specialty>> GetAllAsync(
            CancellationToken ct = default)
        {
            return await DbSet
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }
    }
}

