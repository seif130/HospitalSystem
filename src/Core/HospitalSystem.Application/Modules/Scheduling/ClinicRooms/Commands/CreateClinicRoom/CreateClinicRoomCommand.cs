using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.CreateClinicRoom
{
    public sealed record CreateClinicRoomCommand(
        string RoomNumber,
        Guid DepartmentId,
        int Capacity) : ICommand<Guid>;

}
