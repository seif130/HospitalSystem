using HospitalSystem.Application.Modules.Scheduling.Departments.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Queries.GetDepartments
{
    public sealed class GetDepartmentsQueryHandler
      : IQueryHandler<GetDepartmentsQuery,IReadOnlyList<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departments;

        public GetDepartmentsQueryHandler(IDepartmentRepository departments)
        {
            _departments = departments;
        }

        public async Task<Result<IReadOnlyList<DepartmentDto>>> Handle(
            GetDepartmentsQuery request,CancellationToken cancellationToken)
        {
            var departments = await _departments.GetAllAsync(cancellationToken);

            var result = departments.Select(x => x.ToDto()).ToList();

            return result;
        }
    }


}
