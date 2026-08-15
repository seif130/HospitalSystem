using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.GetDepartments
{
    public sealed class GetDepartmentsQueryHandler : IQueryHandler<GetDepartmentsQuery, IReadOnlyList<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentsRepository;

        public GetDepartmentsQueryHandler(IDepartmentRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        public async Task<Result<IReadOnlyList<DepartmentDto>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentsRepository.GetAllAsync(cancellationToken);
            var dtos = departments.Select(d => d.ToDto()).ToList();

            return Result.Success<IReadOnlyList<DepartmentDto>>(dtos);
        }
    }
}
