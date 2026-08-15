using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Shared.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
