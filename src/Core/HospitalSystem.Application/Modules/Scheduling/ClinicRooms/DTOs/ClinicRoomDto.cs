using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs
{
    public sealed record ClinicRoomDto(
        Guid Id,
        string RoomNumber,
        Guid DepartmentId,
        int Capacity);

}
