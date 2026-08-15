using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.GetDepartments
{
    public sealed record GetDepartmentsQuery : IQuery<IReadOnlyList<DepartmentDto>>;
}
