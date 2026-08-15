using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.AddDoctorAvailability
{
    public sealed class AddDoctorAvailabilityCommandHandler : ICommandHandler<AddDoctorAvailabilityCommand>
    {
        private readonly IDoctorRepository _doctors;
        public AddDoctorAvailabilityCommandHandler(IDoctorRepository doctors) => _doctors = doctors;

        public async Task<Result> Handle(AddDoctorAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctors.GetByIdAsync(new DoctorId(request.DoctorId), cancellationToken);
            if (doctor is null) return Result.Failure(Error.NotFound("Doctor.NotFound", "Doctor not found."));

            try
            {
                var slot = DateRange.Create(request.StartUtc, request.EndUtc);
                doctor.AddAvailability(slot);
            }
            catch (DomainException ex)
            {
                return Result.Failure(Error.Conflict("Doctor.OverlapAvailability", ex.Message));
            }

            return Result.Success();
        }
    }
}
