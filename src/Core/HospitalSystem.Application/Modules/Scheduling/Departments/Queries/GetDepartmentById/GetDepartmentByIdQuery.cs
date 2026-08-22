using HospitalSystem.Application.Modules.Scheduling.Departments.Dto;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Queries.GetDepartmentById
{
    public sealed record GetDepartmentByIdQuery(
        Guid DepartmentId) : IQuery<DepartmentDto>;

}
