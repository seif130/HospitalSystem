using HospitalSystem.Application.Shared.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.CreateClinicRoom
{
    public sealed class CreateClinicRoomCommandHandler
     : ICommandHandler<CreateClinicRoomCommand, Guid>
    {
        private readonly IClinicRoomRepository _rooms;
        private readonly IDepartmentRepository _departments;

        public CreateClinicRoomCommandHandler(
            IClinicRoomRepository rooms,
            IDepartmentRepository departments)
        {
            _rooms = rooms;
            _departments = departments;
        }

        public async Task<Result<Guid>> Handle(
            CreateClinicRoomCommand request,
            CancellationToken cancellationToken)
        {
            var departmentId =
                new DepartmentId(request.DepartmentId);

            var department = await _departments.GetByIdAsync(
                departmentId,
                cancellationToken);

            if (department is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "Department.NotFound",
                        "Department was not found."));
            }

            var exists = await _rooms.ExistsByRoomNumberAsync(
                request.RoomNumber,
                departmentId,
                cancellationToken);

            if (exists)
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "ClinicRoom.AlreadyExists",
                        "A room with this number already exists in the department."));
            }

            var room = ClinicRoom.Create(
                request.RoomNumber,
                departmentId,
                request.Capacity);

            await _rooms.AddAsync(room, cancellationToken);

            return Result.Success(room.Id.Value);
        }
    }

}
