using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.GetClinicRooms
{

    public sealed record GetClinicRoomsQuery : IQuery<IReadOnlyList<ClinicRoomDto>>;
}
