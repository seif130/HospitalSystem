using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.CreateDepartment
{
    public sealed record CreateDepartmentCommand(
        string Name,
        string? Description,
        Guid? HeadDoctorId = null) : IRequest<Result<DepartmentDto>>;
}
