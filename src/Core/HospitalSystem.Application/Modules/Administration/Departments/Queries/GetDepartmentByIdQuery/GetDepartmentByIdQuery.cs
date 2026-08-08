using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Queries.GetDepartmentByIdQuery
{
    public sealed record GetDepartmentByIdQuery(Guid DepartmentId) : IRequest<Result<DepartmentDto>>;
}
