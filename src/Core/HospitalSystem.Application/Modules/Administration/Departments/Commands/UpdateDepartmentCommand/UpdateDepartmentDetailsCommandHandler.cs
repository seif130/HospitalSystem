using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.UpdateDepartmentCommand
{
    public sealed class UpdateDepartmentDetailsCommandHandler : IRequestHandler<UpdateDepartmentDetailsCommand, Result>
    {
        private readonly IHospitalDbContext _db;
        public UpdateDepartmentDetailsCommandHandler(IHospitalDbContext db) => _db = db;

        public async Task<Result> Handle(UpdateDepartmentDetailsCommand request, CancellationToken cancellationToken)
        {
            var department = await _db.Departments
                .SingleOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

            if (department is null)
                return Result.Fail(Error.NotFound("Department.NotFound", "Department was not found."));

            var duplicate = await _db.Departments
                .AnyAsync(x => x.Id != request.DepartmentId && x.Name == request.Name.Trim(), cancellationToken);

            if (duplicate)
                return Result.Fail(Error.Conflict("Department.DuplicateName", "A department with this name already exists."));

            var result = department.UpdateDetails(request.Name.Trim(), request.Description ?? string.Empty, request.HeadDoctorId);
            if (!result.IsSuccess)
                return Result.Fail(result.Errors);

            await _db.SaveChangesAsync(cancellationToken);

            return Result.ok();
        }
    }
}
