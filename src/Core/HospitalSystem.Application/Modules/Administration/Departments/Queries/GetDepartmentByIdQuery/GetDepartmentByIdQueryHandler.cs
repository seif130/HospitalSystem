using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Queries.GetDepartmentByIdQuery
{
    public sealed class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentDto>>
    {
        private readonly IHospitalDbContext _db;
        public GetDepartmentByIdQueryHandler(IHospitalDbContext db) => _db = db;

        public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _db.Departments.AsNoTracking()
                .Include(x => x.Rooms).Include(x => x.Equipments).Include(x => x.Services)
                .Include(x => x.Doctors).Include(x => x.Nurses).Include(x => x.Schedules)
                .SingleOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

            return department is null
                ? Result<DepartmentDto>.Fail(Error.NotFound("Department.NotFound", "Department was not found."))
                : Result<DepartmentDto>.Ok(department.ToDto());
        }
    }
}
