using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddServiceCommand
{
    public sealed record AddServiceCommand(Guid DepartmentId, string ServiceName, string? Description, decimal PriceAmount, string Currency) : IRequest<Result<Guid>>;

}
