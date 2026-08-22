using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Reprository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync( CancellationToken ct = default);
    }

}
