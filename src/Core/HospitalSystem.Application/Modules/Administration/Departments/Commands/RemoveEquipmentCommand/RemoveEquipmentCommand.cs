using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.RemoveEquipmentCommand
{
    public sealed record RemoveEquipmentCommand(Guid DepartmentId, Guid EquipmentId) : IRequest<Result>;
}
