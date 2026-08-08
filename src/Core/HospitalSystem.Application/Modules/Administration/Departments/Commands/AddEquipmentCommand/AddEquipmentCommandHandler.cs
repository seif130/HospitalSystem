using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddEquipmentCommand
{
    public sealed class AddEquipmentCommandHandler : IRequestHandler<AddEquipmentCommand, Result<Guid>>
    {
        private readonly IHospitalDbContext _db;
    public AddEquipmentCommandHandler(IHospitalDbContext db) => _db = db;

    public async Task<Result<Guid>> Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _db.Departments
            .Include(x => x.Equipments)
            .SingleOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

        if (department is null)
            return Result<Guid>.Fail(Error.NotFound("Department.NotFound", "Department was not found."));

        var result = department.AddEquipment(
            request.EquipmentName.Trim(),
            request.SerialNumber.Trim(),
            request.PurchaseDate);

        if (!result.IsSuccess)
            return Result<Guid>.Fail(result.Errors);

        await _db.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Ok(result.Data.Id);
    }
}
}
