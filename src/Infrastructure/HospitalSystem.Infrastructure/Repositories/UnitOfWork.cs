using HospitalSystem.Domain.Reprository;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly SchedulingDbContext _context;

        public UnitOfWork(SchedulingDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return _context.SaveChangesAsync(ct);
        }
    }
}
