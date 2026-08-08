using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Queries.GetDepartmentsQuery
{
    public sealed class GetDepartmentsQueryHandler: IRequestHandler<GetDepartmentsQuery, Result<PaginatedList<DepartmentListItemDto>>>
    {
        private readonly IHospitalDbContext _db;
        public GetDepartmentsQueryHandler(IHospitalDbContext db) => _db = db;

        public async Task<Result<PaginatedList<DepartmentListItemDto>>> Handle(
            GetDepartmentsQuery request,
            CancellationToken cancellationToken)
        {
            var page = Math.Max(request.Page, 1);
            var pageSize = Math.Clamp(request.PageSize, 1, 100);
            var query = _db.Departments.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(x => x.Name.Contains(search) || x.Description.Contains(search));
            }

            var total = await query.CountAsync(cancellationToken);
            var items = await query.OrderBy(x => x.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new DepartmentListItemDto(
                    x.Id, x.Name, x.Description, x.HeadDoctorId,
                    x.Rooms.Count, x.Doctors.Count, x.Nurses.Count, x.Services.Count))
                .ToListAsync(cancellationToken);

            return Result<PaginatedList<DepartmentListItemDto>>.Ok(
                new PaginatedList<DepartmentListItemDto>(items, total, page, pageSize));
        }
    }
}
