using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.ChangeClinicRoomCapacity
{
    public sealed class ChangeClinicRoomCapacityCommandHandler
        : ICommandHandler<ChangeClinicRoomCapacityCommand>
    {
        private readonly IClinicRoomRepository _rooms;

        public ChangeClinicRoomCapacityCommandHandler(
            IClinicRoomRepository rooms)
        {
            _rooms = rooms;
        }

        public async Task<Result> Handle(
            ChangeClinicRoomCapacityCommand request,
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

            room.ChangeCapacity(request.Capacity);

            return Result.Success();
        }
    }

}
