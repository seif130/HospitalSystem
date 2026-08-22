using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetAvailableClinicRooms
{
    public sealed class GetAvailableClinicRoomsQueryHandler
        : IQueryHandler<
            GetAvailableClinicRoomsQuery,
            IReadOnlyList<ClinicRoomDto>>
    {
        private readonly IClinicRoomRepository _rooms;

        public GetAvailableClinicRoomsQueryHandler(
            IClinicRoomRepository rooms)
        {
            _rooms = rooms;
        }

        public async Task<Result<IReadOnlyList<ClinicRoomDto>>> Handle(
            GetAvailableClinicRoomsQuery request,
            CancellationToken cancellationToken)
        {
            var period = DateRange.Create(
                request.FromUtc,
                request.ToUtc);

            var rooms = await _rooms.GetAvailableRoomsAsync(
                new DepartmentId(request.DepartmentId),
                period,
                cancellationToken);

            return rooms
                .Select(x => x.ToDto())
                .ToList();
        }
    }

}
