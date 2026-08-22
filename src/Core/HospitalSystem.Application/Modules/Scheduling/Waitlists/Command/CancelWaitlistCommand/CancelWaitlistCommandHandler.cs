using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.CancelWaitlistCommand
{
    public sealed class CancelWaitlistCommandHandler: ICommandHandler<CancelWaitlistCommand>
    {
        private readonly IWaitlistRepository _waitlists;

        public CancelWaitlistCommandHandler(
            IWaitlistRepository waitlists)
        {
            _waitlists = waitlists;
        }

        public async Task<Result> Handle(CancelWaitlistCommand request,CancellationToken cancellationToken)
        {
            var waitlist = await _waitlists.GetByIdAsync(
                new WaitlistId(request.WaitlistId),cancellationToken);

            if (waitlist is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Waitlist.NotFound","Waitlist entry was not found."));
            }

            waitlist.Cancel();

            return Result.Success();
        }
    }

}
