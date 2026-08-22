using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.ChangeClinicRoomDepartment
{
    public sealed record ChangeClinicRoomDepartmentCommand(
        Guid ClinicRoomId,
        Guid DepartmentId) : ICommand;

}
