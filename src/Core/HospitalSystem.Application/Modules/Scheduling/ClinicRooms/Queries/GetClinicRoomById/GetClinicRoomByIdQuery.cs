using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetClinicRoomById
{
    public sealed record GetClinicRoomByIdQuery(
        Guid ClinicRoomId) : IQuery<ClinicRoomDto>;

}
