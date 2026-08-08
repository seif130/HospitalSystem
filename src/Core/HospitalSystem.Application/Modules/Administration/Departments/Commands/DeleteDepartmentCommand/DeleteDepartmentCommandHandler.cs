using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.DeleteDepartmentCommand
{
    public sealed class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result>
    {
        private readonly IHospitalDbContext _db;

        public DeleteDepartmentCommandHandler(IHospitalDbContext db) => _db = db;

        public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _db.Departments
                .SingleOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

            if (department is null)
                return Result.Fail(Error.NotFound("Department.NotFound", "Department was not found."));


            _db.Departments.Remove(department);

            await _db.SaveChangesAsync(cancellationToken);

            return Result.ok();
        }
    }
}
