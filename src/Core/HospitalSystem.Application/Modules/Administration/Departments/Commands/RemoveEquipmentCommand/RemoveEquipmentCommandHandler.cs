using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.RemoveEquipmentCommand
{
    public sealed class RemoveEquipmentCommandHandler : IRequestHandler<RemoveEquipmentCommand, Result>
    {
        private readonly IHospitalDbContext _db;
        public RemoveEquipmentCommandHandler(IHospitalDbContext db) => _db = db;

        public async Task<Result> Handle(RemoveEquipmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _db.Departments
                .Include(x => x.Equipments)
                .SingleOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

            if (department is null)
                return Result.Fail(Error.NotFound("Department.NotFound", "Department was not found."));

            var equipment = department.Equipments.FirstOrDefault(e => e.Id == request.EquipmentId);
            if (equipment is null)
                return Result.Fail(Error.NotFound("Equipment.NotFound", "Equipment was not found in this department."));

            department.RemoveEquipment(request.EquipmentId);

            await _db.SaveChangesAsync(cancellationToken);
            return Result.ok();
        }
    }
}
