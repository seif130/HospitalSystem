using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs
{
    public static class ClinicRoomMappings
    {
        public static ClinicRoomDto ToDto(this ClinicRoom room)
        {
            return new ClinicRoomDto(
                room.Id.Value,
                room.RoomNumber,
                room.DepartmentId.Value,
                room.Capacity);
        }
    }

}
