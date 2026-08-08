using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Queries.GetDepartmentsQuery
{
    public sealed record GetDepartmentsQuery(string? Search = null, int Page = 1, int PageSize = 20)
        : IRequest<Result<PaginatedList<DepartmentListItemDto>>>;
}
