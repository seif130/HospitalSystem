using HospitalSystem.Application.Shared.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.CreateClinicRoom
{
    public sealed class CreateClinicRoomCommandHandler : ICommandHandler<CreateClinicRoomCommand, Guid>
    {
        private readonly IClinicRoomRepository _roomsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateClinicRoomCommandHandler(IClinicRoomRepository roomsRepository, IUnitOfWork unitOfWork)
        {
            _roomsRepository = roomsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateClinicRoomCommand request, CancellationToken cancellationToken)
        {
            ClinicRoom room;
            try
            {
                room = ClinicRoom.Create(request.RoomNumber, request.Description);
            }
            catch (DomainException ex)
            {
                return Result.Failure<Guid>(Error.Conflict("ClinicRoom.CannotCreate", ex.Message));
            }

            _roomsRepository.Add(room);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(room.Id.Value);
        }
    }
}
