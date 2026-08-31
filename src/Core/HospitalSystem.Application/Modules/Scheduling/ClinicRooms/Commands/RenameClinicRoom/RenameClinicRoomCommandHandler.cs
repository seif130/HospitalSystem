using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.RenameClinicRoom
{
    public sealed class RenameClinicRoomCommandHandler: ICommandHandler<RenameClinicRoomCommand>
    {
        private readonly IClinicRoomRepository _rooms;
        private readonly IUnitOfWork _unitOfWork;

        public RenameClinicRoomCommandHandler(
            IClinicRoomRepository rooms,
            IUnitOfWork unitOfWork)
        {
            _rooms = rooms;
            _unitOfWork = unitOfWork;
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

            var normalizedRoomNumber = request.RoomNumber.Trim();

            if (string.Equals(
                room.RoomNumber,
                normalizedRoomNumber,
                StringComparison.OrdinalIgnoreCase))
            {
                return Result.Success();
            }

            var exists = await _rooms.ExistsByRoomNumberAsync(
                normalizedRoomNumber,
                room.DepartmentId,
                cancellationToken);

            if (exists)
            {
                return Result.Failure(
                    Error.Conflict(
                        "ClinicRoom.AlreadyExists",
                        "A room with this number already exists in the department."));
            }

            room.Rename(normalizedRoomNumber);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
