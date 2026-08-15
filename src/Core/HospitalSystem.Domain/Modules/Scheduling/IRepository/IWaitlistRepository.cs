using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.IRepository
{
    public interface IWaitlistRepository
    {
        Task<Waitlist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Waitlist waitlistEntry);
    }
}
