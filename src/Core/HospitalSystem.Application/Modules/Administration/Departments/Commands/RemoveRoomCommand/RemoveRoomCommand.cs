using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.RemoveRoomCommand
{
    public sealed record RemoveRoomCommand(Guid DepartmentId, Guid RoomId) : IRequest<Result>;
}
