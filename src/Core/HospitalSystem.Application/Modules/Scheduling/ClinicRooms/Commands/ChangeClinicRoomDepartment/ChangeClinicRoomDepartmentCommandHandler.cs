using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.ChangeClinicRoomDepartment
{
    public sealed class ChangeClinicRoomDepartmentCommandHandler
        : ICommandHandler<ChangeClinicRoomDepartmentCommand>
    {
        private readonly IClinicRoomRepository _rooms;
        private readonly IDepartmentRepository _departments;

        public ChangeClinicRoomDepartmentCommandHandler(
            IClinicRoomRepository rooms,
            IDepartmentRepository departments)
        {
            _rooms = rooms;
            _departments = departments;
        }

        public async Task<Result> Handle(
            ChangeClinicRoomDepartmentCommand request,
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

            var departmentId =
                new DepartmentId(request.DepartmentId);

            var department = await _departments.GetByIdAsync(
                departmentId,
                cancellationToken);

            if (department is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Department.NotFound",
                        "Department was not found."));
            }

            var exists = await _rooms.ExistsByRoomNumberAsync(
                room.RoomNumber,
                departmentId,
                cancellationToken);

            if (exists)
            {
                return Result.Failure(
                    Error.Conflict(
                        "ClinicRoom.AlreadyExists",
                        "A room with this number already exists in the target department."));
            }

            room.ChangeDepartment(departmentId);

            return Result.Success();
        }
    }

}
