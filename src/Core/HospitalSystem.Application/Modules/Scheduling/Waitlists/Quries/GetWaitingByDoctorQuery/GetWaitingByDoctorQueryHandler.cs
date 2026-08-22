using HospitalSystem.Application.Modules.Scheduling.Waitlists.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitingByDoctorQuery
{
    public sealed class GetWaitingByDoctorQueryHandler
        : IQueryHandler<GetWaitingByDoctorQuery,IReadOnlyList<WaitlistDto>>
    {
        private readonly IWaitlistRepository _waitlists;

        public GetWaitingByDoctorQueryHandler(IWaitlistRepository waitlists)
        {
            _waitlists = waitlists;
        }

        public async Task<Result<IReadOnlyList<WaitlistDto>>> Handle(
            GetWaitingByDoctorQuery request,CancellationToken cancellationToken)
        {
            var period = DateRange.Create(request.FromUtc,request.ToUtc);

            var entries = await _waitlists.GetWaitingByDoctorAsync(
                new DoctorId(request.DoctorId),
                period,cancellationToken);

            return entries.Select(x => x.ToDto()).ToList();
        }
    }

}
