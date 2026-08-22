using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.ChangeClinicRoomCapacity
{
    public sealed record ChangeClinicRoomCapacityCommand(
        Guid ClinicRoomId,
        int Capacity) : ICommand;

}
