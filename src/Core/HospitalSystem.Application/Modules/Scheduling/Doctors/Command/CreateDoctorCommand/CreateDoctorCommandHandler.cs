using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Departments.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using HospitalSystem.Domain.Reprository;
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
        private readonly ISpecialtyRepository _specialties;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDoctorCommandHandler(
            IDoctorRepository doctors,
            IDepartmentRepository departments,
            ISpecialtyRepository specialties,
            IUnitOfWork unitOfWork)
        {
            _doctors = doctors;
            _departments = departments;
            _specialties = specialties;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            CreateDoctorCommand request,
            CancellationToken cancellationToken)
        {
            var departmentId =
                new DepartmentId(request.DepartmentId);

            var department = await _departments.GetByIdAsync(
                departmentId,
                cancellationToken);

            if (department is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "Department.NotFound",
                        "Department was not found."));
            }

            var specialtyId =
                new SpecialtyId(request.SpecialtyId);

            var specialty = await _specialties.GetByIdAsync(
                specialtyId,
                cancellationToken);

            if (specialty is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "Specialty.NotFound",
                        "Specialty was not found."));
            }

            if (!specialty.IsActive)
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "Specialty.Inactive",
                        "The selected specialty is inactive."));
            }

            var exists =
                await _doctors.ExistsByLicenseNumberAsync(
                    request.LicenseNumber,
                    cancellationToken);

            if (exists)
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "Doctor.LicenseAlreadyExists",
                        "A doctor with this license number already exists."));
            }

            var name = PersonName.Create(
                request.FirstName,
                request.LastName);

            var doctor = Doctor.Register(
                name,
                specialtyId,
                departmentId,
                request.LicenseNumber);

            await _doctors.AddAsync(
                doctor,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success(
                doctor.Id.Value);
        }
    }

}
