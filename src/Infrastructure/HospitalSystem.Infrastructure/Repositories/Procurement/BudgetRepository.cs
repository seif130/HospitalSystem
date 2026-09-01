using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Budget.Contract;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Infrastructure.Contexts.DbContextsExtended;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Repositories.Procurement
{

    public sealed class BudgetRepository: Repository<Budget, BudgetId>,
        IBudgetRepository
    {
        private readonly ProcurementDbContext _context;

        public BudgetRepository(
            ProcurementDbContext context): base(context)
        {
            _context = context;
        }

        public Task<bool> ExistsOverlappingPeriodAsync(
            DepartmentId departmentId,
            DateRange fiscalPeriod,
            CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(fiscalPeriod);

            return DbSet.AnyAsync(
                budget =>
                    budget.DepartmentId == departmentId &&
                    budget.FiscalPeriod.Start <
                        fiscalPeriod.End!.Value &&
                    fiscalPeriod.Start <
                        budget.FiscalPeriod.End!.Value,
                ct);
        }

        public async Task<IReadOnlyList<Budget>> GetByDepartmentAsync(
            DepartmentId departmentId,
            CancellationToken ct = default)
        {
            return await DbSet
                .AsNoTracking()
                .Where(x => x.DepartmentId == departmentId)
                .OrderByDescending(x => x.FiscalPeriod.Start)
                .ToListAsync(ct);
        }
    }
}
