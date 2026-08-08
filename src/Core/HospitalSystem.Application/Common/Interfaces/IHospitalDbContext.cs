using HospitalSystem.Domain.Modules.Administration.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Application.Common.Interfaces;

public interface IHospitalDbContext
{
    DbSet<Department> Departments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IApplicationUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
