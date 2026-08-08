using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddRoomCommand
{
    public sealed record AddRoomCommand(Guid DepartmentId, string RoomNumber, RoomType Type) : IRequest<Result<Guid>>;
}
