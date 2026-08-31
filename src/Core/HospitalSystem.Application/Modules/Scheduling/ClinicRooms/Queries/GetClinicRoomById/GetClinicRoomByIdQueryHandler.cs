using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetClinicRoomById
{
    public sealed class GetClinicRoomByIdQueryHandler: IQueryHandler<GetClinicRoomByIdQuery, ClinicRoomDto>
    {
        private readonly IClinicRoomRepository _rooms;

        public GetClinicRoomByIdQueryHandler(
            IClinicRoomRepository rooms)
        {
            _rooms = rooms;
        }

        public async Task<Result<ClinicRoomDto>> Handle(GetClinicRoomByIdQuery request,
            CancellationToken cancellationToken)
        {
            var room = await _rooms.GetByIdAsync(
                new ClinicRoomId(request.ClinicRoomId),cancellationToken);

            if (room is null)
            {
                return Result.Failure<ClinicRoomDto>(
                    Error.NotFound("ClinicRoom.NotFound",
                        "Clinic room was not found."));
            }

            return room.ToDto();
        }
    }

}
