using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.RemoveRoomCommand
{
    public sealed class RemoveRoomCommandHandler : IRequestHandler<RemoveRoomCommand, Result>
    {
        private readonly IHospitalDbContext _db;
        public RemoveRoomCommandHandler(IHospitalDbContext db) => _db = db;

        public async Task<Result> Handle(RemoveRoomCommand request, CancellationToken cancellationToken)
        {
            var department = await _db.Departments
                .Include(x => x.Rooms)
                .SingleOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

            if (department is null)
                return Result.Fail(Error.NotFound("Department.NotFound", "Department was not found."));

            var result = department.RemoveRoom(request.RoomId);
            if (!result.IsSuccess)
                return Result.Fail(result.Errors);

            await _db.SaveChangesAsync(cancellationToken);
            return Result.ok();
        }
    }
}
