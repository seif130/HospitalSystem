using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.DeleteDepartmentCommand
{
    public sealed record DeleteDepartmentCommand(Guid DepartmentId) : IRequest<Result>;
}
