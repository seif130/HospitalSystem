using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.RenameClinicRoom
{
    public sealed record RenameClinicRoomCommand(
        Guid ClinicRoomId,
        string RoomNumber) : ICommand;

}
