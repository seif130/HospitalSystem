using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetClinicRoomsByDepartment
{
    public sealed record GetClinicRoomsByDepartmentQuery(
        Guid DepartmentId) : IQuery<IReadOnlyList<ClinicRoomDto>>;

}
