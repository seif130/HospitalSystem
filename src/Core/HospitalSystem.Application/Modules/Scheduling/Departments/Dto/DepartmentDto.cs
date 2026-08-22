using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Dto
{
    public sealed record DepartmentDto(
        Guid Id,
        string Name,
        string? Description);

}
