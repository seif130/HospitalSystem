using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorNameCommand
{
    public sealed class ChangeDoctorNameCommandHandler
        : ICommandHandler<ChangeDoctorNameCommand>
    {
        private readonly IDoctorRepository _doctors;

        public ChangeDoctorNameCommandHandler(
            IDoctorRepository doctors)
        {
            _doctors = doctors;
        }

        public async Task<Result> Handle(
            ChangeDoctorNameCommand request,
            CancellationToken cancellationToken)
        {
            var doctor = await _doctors.GetByIdAsync(
                new DoctorId(request.DoctorId),
                cancellationToken);

            if (doctor is null)
            {
                return Result.Failure(
                    Error.NotFound("Doctor.NotFound","Doctor was not found."));
            }

            var name = PersonName.Create(
                request.FirstName,request.LastName);

            doctor.ChangeName(name);

            return Result.Success();
        }
    }

}
