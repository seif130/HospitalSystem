using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorDepartmentCommand
{
    public sealed class ChangeDoctorDepartmentCommandHandler: ICommandHandler<ChangeDoctorDepartmentCommand>
    {
        private readonly IDoctorRepository _doctors;
        private readonly IDepartmentRepository _departments;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeDoctorDepartmentCommandHandler(
            IDoctorRepository doctors,
            IDepartmentRepository departments,
            IUnitOfWork unitOfWork)
        {
            _doctors = doctors;
            _departments = departments;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ChangeDoctorDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var doctor = await _doctors.GetByIdAsync(
                new DoctorId(request.DoctorId),
                cancellationToken);

            if (doctor is null)
            {
                return Result.Failure(
                    Error.NotFound("Doctor.NotFound",
                        "Doctor was not found."));
            }

            var departmentId = new DepartmentId(request.DepartmentId);

            if (doctor.DepartmentId == departmentId)
            {
                return Result.Success();
            }

            var department = await _departments.GetByIdAsync(
                departmentId,
                cancellationToken);

            if (department is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Department.NotFound",
                        "Department was not found."));
            }

            doctor.ChangeDepartment(departmentId);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
