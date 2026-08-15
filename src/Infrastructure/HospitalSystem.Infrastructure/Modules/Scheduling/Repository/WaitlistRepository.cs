using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Scheduling.Repository
{
    internal sealed class WaitlistRepository : IWaitlistRepository
    {
        private readonly SchedulingDbContext _context;

        public WaitlistRepository(SchedulingDbContext context) => _context = context;

        public async Task<Waitlist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var waitlistId = new WaitlistId(id); 

            return await _context.Waitlists
                .FirstOrDefaultAsync(w => w.Id == waitlistId, cancellationToken);
        }

        public void Add(Waitlist waitlistEntry)
        {
            _context.Waitlists.Add(waitlistEntry);
        }
    }
}
