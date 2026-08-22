using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Command.RenameDepartment
{
    public sealed class RenameDepartmentCommandHandler
       : ICommandHandler<RenameDepartmentCommand>
    {
        private readonly IDepartmentRepository _departments;

        public RenameDepartmentCommandHandler(
            IDepartmentRepository departments)
        {
            _departments = departments;
        }

        public async Task<Result> Handle(
            RenameDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var department = await _departments.GetByIdAsync(
                new DepartmentId(request.DepartmentId),
                cancellationToken);

            if (department is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Department.NotFound",
                        "Department was not found."));
            }

            var normalizedName = request.Name.Trim();

            if (string.Equals(
                department.Name,
                normalizedName,
                StringComparison.OrdinalIgnoreCase))
            {
                return Result.Success();
            }

            var exists = await _departments.ExistsByNameAsync(
                normalizedName,
                cancellationToken);

            if (exists)
            {
                return Result.Failure(
                    Error.Conflict(
                        "Department.AlreadyExists",
                        "A department with this name already exists."));
            }

            department.Rename(normalizedName);

            return Result.Success();
        }
    }

}
