using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorSpecialtyCommand
{
    public sealed class ChangeDoctorSpecialtyCommandHandler
       : ICommandHandler<ChangeDoctorSpecialtyCommand>
    {
        private readonly IDoctorRepository _doctors;
        private readonly ISpecialtyRepository _specialties;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeDoctorSpecialtyCommandHandler(
            IDoctorRepository doctors,
            ISpecialtyRepository specialties,
            IUnitOfWork unitOfWork)
        {
            _doctors = doctors;
            _specialties = specialties;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ChangeDoctorSpecialtyCommand request,
            CancellationToken cancellationToken)
        {
            var doctor = await _doctors.GetByIdAsync(
                new DoctorId(request.DoctorId),
                cancellationToken);

            if (doctor is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Doctor.NotFound",
                        "Doctor was not found."));
            }

            var specialtyId = new SpecialtyId(request.SpecialtyId);

            var specialty = await _specialties.GetByIdAsync(
                specialtyId,
                cancellationToken);

            if (specialty is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Specialty.NotFound",
                        "Specialty was not found."));
            }

            doctor.ChangeSpecialty(specialtyId);

            if (!specialty.IsActive)
            {
                return Result.Failure(
                    Error.Conflict(
                        "Specialty.Inactive",
                        "The selected specialty is inactive."));
            }

            doctor.ChangeSpecialty(specialtyId);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }

}
