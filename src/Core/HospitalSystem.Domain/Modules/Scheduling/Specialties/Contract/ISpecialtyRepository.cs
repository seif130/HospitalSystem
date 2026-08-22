using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract
{

    public interface ISpecialtyRepository : IRepository<Specialty, SpecialtyId>
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
    }
}
