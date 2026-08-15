using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.JoinWaitlist
{
    public sealed class JoinWaitlistCommandHandler : ICommandHandler<JoinWaitlistCommand, Guid>
    {
        private readonly IWaitlistRepository _waitlists;
        public JoinWaitlistCommandHandler(IWaitlistRepository waitlists) => _waitlists = waitlists;

        public async Task<Result<Guid>> Handle(JoinWaitlistCommand request, CancellationToken cancellationToken)
        {
            var waitlist = Waitlist.Join(
                new PatientId(request.PatientId),
                new DoctorId(request.DoctorId),
                request.PreferredFromUtc,
                request.PreferredToUtc);

            _waitlists.Add(waitlist);
            return Result.Success(waitlist.Id.Value);
        }
    }
}
