using HospitalSystem.Application.Modules.Scheduling.Departments.Dto;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Queries.GetDepartments
{
    public sealed record GetDepartmentsQuery
      : IQuery<IReadOnlyList<DepartmentDto>>;

}
