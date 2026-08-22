using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.JoinWaitlistCommand
{
    public sealed class JoinWaitlistCommandHandler
     : ICommandHandler<JoinWaitlistCommand, Guid>
    {
        private readonly IWaitlistRepository _waitlists;
        private readonly IDoctorRepository _doctors;

        public JoinWaitlistCommandHandler(
            IWaitlistRepository waitlists,IDoctorRepository doctors)
        {
            _waitlists = waitlists;
            _doctors = doctors;
        }

        public async Task<Result<Guid>> Handle(
            JoinWaitlistCommand request,
            CancellationToken cancellationToken)
        {
            var patientId = new PatientId(request.PatientId);
            var doctorId = new DoctorId(request.DoctorId);

            var doctor = await _doctors.GetByIdAsync(
                doctorId,
                cancellationToken);

            if (doctor is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "Doctor.NotFound",
                        "Doctor was not found."));
            }

            var hasActiveEntry =
                await _waitlists.HasActiveEntryAsync(
                    patientId,
                    doctorId,
                    cancellationToken);

            if (hasActiveEntry)
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "Waitlist.AlreadyExists",
                        "The patient already has an active waitlist entry for this doctor."));
            }

            var waitlist = Waitlist.Join(
                patientId,
                doctorId,
                request.PreferredFromUtc,
                request.PreferredToUtc);

            await _waitlists.AddAsync(waitlist, cancellationToken);

            return waitlist.Id.Value;
        }
    }


}
