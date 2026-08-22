using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorLicenseNumberCommand
{
    public sealed class ChangeDoctorLicenseNumberCommandHandler: ICommandHandler<ChangeDoctorLicenseNumberCommand>
    {
        private readonly IDoctorRepository _doctors;

        public ChangeDoctorLicenseNumberCommandHandler(
            IDoctorRepository doctors)
        {
            _doctors = doctors;
        }

        public async Task<Result> Handle(
            ChangeDoctorLicenseNumberCommand request,
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

            var normalizedLicenseNumber =
                request.LicenseNumber.Trim();

            if (string.Equals(
                doctor.LicenseNumber,
                normalizedLicenseNumber,
                StringComparison.OrdinalIgnoreCase))
            {
                return Result.Success();
            }

            var exists = await _doctors.ExistsByLicenseNumberAsync(
                normalizedLicenseNumber,
                cancellationToken);

            if (exists)
            {
                return Result.Failure(
                    Error.Conflict(
                        "Doctor.LicenseAlreadyExists",
                        "A doctor with this license number already exists."));
            }

            doctor.ChangeLicenseNumber(
                normalizedLicenseNumber);

            return Result.Success();
        }

    }
}