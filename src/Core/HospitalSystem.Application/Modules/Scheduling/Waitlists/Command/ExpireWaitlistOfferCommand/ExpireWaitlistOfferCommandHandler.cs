using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.ExpireWaitlistOfferCommand
{
    public sealed class ExpireWaitlistOfferCommandHandler
        : ICommandHandler<ExpireWaitlistOfferCommand>
    {
        private readonly IWaitlistRepository _waitlists;
        private readonly IUnitOfWork _unitOfWork;

        public ExpireWaitlistOfferCommandHandler(
            IWaitlistRepository waitlists,
            IUnitOfWork unitOfWork)
        {
            _waitlists = waitlists;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ExpireWaitlistOfferCommand request, CancellationToken cancellationToken = default)
        {
            var waitlist = await _waitlists.GetByIdAsync(
                new WaitlistId(request.WaitlistId),
                cancellationToken);

            if (waitlist is null)
            {
                return Result.Failure(
                    Error.NotFound("Waitlist.NotFound",
                        "Waitlist entry was not found."));
            }

            waitlist.ExpireOffer();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
