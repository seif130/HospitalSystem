using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetAvailableClinicRooms
{
    public sealed record GetAvailableClinicRoomsQuery(
        Guid DepartmentId,
        DateTime FromUtc,
        DateTime ToUtc)
        : IQuery<IReadOnlyList<ClinicRoomDto>>;

}
