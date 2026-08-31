using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Departments;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Command.CreateDepartment
{
    public sealed class CreateDepartmentCommandHandler: ICommandHandler<CreateDepartmentCommand, Guid>
    {
        private readonly IDepartmentRepository _departments;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentCommandHandler(IDepartmentRepository departments,IUnitOfWork unitOfWork)
        {
            _departments = departments;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var normalizedName = request.Name.Trim();

            var exists = await _departments.ExistsByNameAsync(normalizedName,cancellationToken);

            if (exists)
            {
                return Result.Failure<Guid>(
                    Error.Conflict("Department.AlreadyExists","A department with this name already exists."));
            }

            var department = Department.Create(normalizedName,request.Description);

            await _departments.AddAsync(department,cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success(
                department.Id.Value);
        }
    }
}