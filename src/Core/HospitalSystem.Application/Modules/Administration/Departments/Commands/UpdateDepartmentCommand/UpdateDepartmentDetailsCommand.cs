using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.UpdateDepartmentCommand
{
    public sealed record UpdateDepartmentDetailsCommand(
     Guid DepartmentId,
     string Name,
     string? Description,
     Guid? HeadDoctorId) : IRequest<Result>;
}
