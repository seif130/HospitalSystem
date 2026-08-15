using HospitalSystem.Application.Shared.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Departments;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.CreateDepartment
{
    public sealed class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, Guid>
    {
        private readonly IDepartmentRepository _departments;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentCommandHandler(IDepartmentRepository departments, IUnitOfWork unitOfWork)
        {
            _departments = departments;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department;
            try
            {
                department = Department.Create(request.Name, request.Description);
            }
            catch (DomainException ex)
            {
                return Result.Failure<Guid>(Error.Conflict("Department.CannotCreate", ex.Message));
            }

            _departments.Add(department);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(department.Id.Value);
        }
    }
}
