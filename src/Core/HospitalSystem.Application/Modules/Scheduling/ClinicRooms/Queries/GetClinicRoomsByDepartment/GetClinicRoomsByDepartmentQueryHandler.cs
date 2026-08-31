using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetClinicRoomsByDepartment
{
    public sealed class GetClinicRoomsByDepartmentQueryHandler
      : IQueryHandler<GetClinicRoomsByDepartmentQuery,IReadOnlyList<ClinicRoomDto>>
    {
        private readonly IClinicRoomRepository _rooms;

        public GetClinicRoomsByDepartmentQueryHandler(IClinicRoomRepository rooms)
        {
            _rooms = rooms;
        }

        public async Task<Result<IReadOnlyList<ClinicRoomDto>>> Handle(
            GetClinicRoomsByDepartmentQuery request,CancellationToken cancellationToken)
        {
            var rooms = await _rooms.GetByDepartmentAsync(
                new DepartmentId(request.DepartmentId),cancellationToken);

            return rooms.Select(x => x.ToDto()).ToList();
        }
    }

}
