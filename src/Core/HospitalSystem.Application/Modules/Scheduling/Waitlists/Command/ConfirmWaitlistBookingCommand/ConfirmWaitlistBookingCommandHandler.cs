using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.ConfirmWaitlistBookingCommand
{
    public sealed class ConfirmWaitlistBookingCommandHandler: ICommandHandler<ConfirmWaitlistBookingCommand>
    {
        private readonly IWaitlistRepository _waitlists;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmWaitlistBookingCommandHandler(
            IWaitlistRepository waitlists, IUnitOfWork unitOfWork)
        {
            _waitlists = waitlists;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ConfirmWaitlistBookingCommand request, CancellationToken cancellationToken = default)
        {
            var waitlist = await _waitlists.GetByIdAsync(
                new WaitlistId(request.WaitlistId),cancellationToken);

            if (waitlist is null)
            {
                return Result.Failure(
                    Error.NotFound( "Waitlist.NotFound",
                        "Waitlist entry was not found."));
            }

            waitlist.ConfirmBooking();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
