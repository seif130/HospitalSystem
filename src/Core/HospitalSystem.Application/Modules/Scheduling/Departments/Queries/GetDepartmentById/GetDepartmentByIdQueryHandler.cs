using HospitalSystem.Application.Modules.Scheduling.Departments.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Queries.GetDepartmentById
{
   public sealed class GetDepartmentByIdQueryHandler
        : IQueryHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        private readonly IDepartmentRepository _departments;

        public GetDepartmentByIdQueryHandler(IDepartmentRepository departments)
        {
            _departments = departments;
        }

        public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request,CancellationToken cancellationToken = default)
        {
            var department = await _departments.GetByIdAsync(new DepartmentId(request.DepartmentId),cancellationToken);

            if (department is null)
            {
                return Result.Failure<DepartmentDto>(
                    Error.NotFound("Department.NotFound","Department was not found."));
            }

            return Result.Success(department.ToDto());
        }
    }

}
