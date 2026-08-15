using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.GetClinicRooms
{
    public sealed class GetClinicRoomsQueryHandler : IQueryHandler<GetClinicRoomsQuery, IReadOnlyList<ClinicRoomDto>>
    {
        private readonly IClinicRoomRepository _roomsRepository;

        public GetClinicRoomsQueryHandler(IClinicRoomRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public async Task<Result<IReadOnlyList<ClinicRoomDto>>> Handle(GetClinicRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _roomsRepository.GetAllAsync(cancellationToken);
            var dtos = rooms.Select(r => r.ToDto()).ToList();

            return Result.Success<IReadOnlyList<ClinicRoomDto>>(dtos);
        }
    }
}
