using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddRoomCommand
{
    public sealed class AddRoomCommandHandler : IRequestHandler<AddRoomCommand, Result<Guid>>
    {
        private readonly IHospitalDbContext _db;
        public AddRoomCommandHandler(IHospitalDbContext db) => _db = db;

        public async Task<Result<Guid>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {
            var department = await _db.Departments
                .Include(x => x.Rooms)
                .SingleOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

            if (department is null)
                return Result<Guid>.Fail(Error.NotFound("Department.NotFound", "Department was not found."));

            var result = department.AddRoom(request.RoomNumber.Trim(), request.Type);
            if (!result.IsSuccess)
                return Result<Guid>.Fail(result.Errors);

            await _db.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(result.Data.Id);
        }
    }
}
