using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.GetClinicRooms
{
    public sealed record ClinicRoomDto(Guid Id, string RoomNumber, bool IsAvailable);
}
