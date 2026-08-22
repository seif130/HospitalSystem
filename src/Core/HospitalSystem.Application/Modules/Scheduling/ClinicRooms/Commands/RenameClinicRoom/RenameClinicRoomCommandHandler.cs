using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.RenameClinicRoom
{
    public sealed class RenameClinicRoomCommandHandler
        : ICommandHandler<RenameClinicRoomCommand>
    {
        private readonly IClinicRoomRepository _rooms;

        public RenameClinicRoomCommandHandler(
            IClinicRoomRepository rooms)
        {
            _rooms = rooms;
        }

        public async Task<Result> Handle(
            RenameClinicRoomCommand request,
            CancellationToken cancellationToken)
        {
            var room = await _rooms.GetByIdAsync(
                new ClinicRoomId(request.ClinicRoomId),
                cancellationToken);

            if (room is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "ClinicRoom.NotFound",
                        "Clinic room was not found."));
            }

            var exists = await _rooms.ExistsByRoomNumberAsync(
                request.RoomNumber,
                room.DepartmentId,
                cancellationToken);

            if (exists && room.RoomNumber != request.RoomNumber.Trim())
            {
                return Result.Failure(
                    Error.Conflict(
                        "ClinicRoom.AlreadyExists",
                        "A room with this number already exists in the department."));
            }

            room.Rename(request.RoomNumber);

            return Result.Success();
        }
    }

}
