using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.CreateDepartment
{
    public sealed class CreateDepartmentCommandHandler
        : IRequestHandler<CreateDepartmentCommand, Result<DepartmentDto>>
    {
        private readonly IHospitalDbContext _db;

        public CreateDepartmentCommandHandler(IHospitalDbContext db) => _db = db;

        public async Task<Result<DepartmentDto>> Handle(
            CreateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var name = request.Name.Trim();
            var exists = await _db.Departments.AnyAsync(x => x.Name == name, cancellationToken);

            if (exists)
                return Result<DepartmentDto>.Fail(
                    Error.Conflict("Department.DuplicateName", "A department with this name already exists."));

            var result = Department.Create(name, request.Description ?? string.Empty, request.HeadDoctorId);
            if (!result.IsSuccess)
                return Result<DepartmentDto>.Fail(result.Errors);

            _db.Departments.Add(result.Data);
            await _db.SaveChangesAsync(cancellationToken);

            return Result<DepartmentDto>.Ok(result.Data.ToDto());
        }
    }
}
