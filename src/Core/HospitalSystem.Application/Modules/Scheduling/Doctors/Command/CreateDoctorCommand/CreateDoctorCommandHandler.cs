using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.CreateDoctorCommand
{
    public sealed class CreateDoctorCommandHandler
        : ICommandHandler<CreateDoctorCommand, Guid>
    {
        private readonly IDoctorRepository _doctors;
        private readonly IDepartmentRepository _departments;

        public CreateDoctorCommandHandler(
            IDoctorRepository doctors,IDepartmentRepository departments)
        {
            _doctors = doctors;
            _departments = departments;
        }

        public async Task<Result<Guid>> Handle(
            CreateDoctorCommand request,
            CancellationToken cancellationToken)
        {
            var departmentId =
                new DepartmentId(request.DepartmentId);

            var department = await _departments.GetByIdAsync(departmentId,cancellationToken);

            if (department is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "Department.NotFound","Department was not found."));
            }

            var exists = await _doctors.ExistsByLicenseNumberAsync(
                request.LicenseNumber,cancellationToken);

            if (exists)
            {
                return Result.Failure<Guid>(
                    Error.Conflict("Doctor.LicenseAlreadyExists",
                        "A doctor with this license number already exists."));
            }

            var name = PersonName.Create(request.FirstName,request.LastName);

            var doctor = Doctor.Register(
                name,request.Specialty,
                departmentId,request.LicenseNumber);

           await _doctors.AddAsync(doctor, cancellationToken);

            return doctor.Id.Value;
        }
    }

}
