using HospitalSystem.Application.Modules.Scheduling.Waitlists.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitlistEntryByIdQuery
{
    public sealed class GetWaitlistByIdQueryHandler
        : IQueryHandler<GetWaitlistByIdQuery, WaitlistDto>
    {
        private readonly IWaitlistRepository _waitlists;

        public GetWaitlistByIdQueryHandler(
            IWaitlistRepository waitlists)
        {
            _waitlists = waitlists;
        }

        public async Task<Result<WaitlistDto>> Handle(
            GetWaitlistByIdQuery request,
            CancellationToken cancellationToken)
        {
            var waitlist = await _waitlists.GetByIdAsync(
                new WaitlistId(request.WaitlistId),
                cancellationToken);

            if (waitlist is null)
            {
                return Result.Failure<WaitlistDto>(
                    Error.NotFound(
                        "Waitlist.NotFound",
                        "Waitlist entry was not found."));
            }

            return waitlist.ToDto();
        }
    }

}
