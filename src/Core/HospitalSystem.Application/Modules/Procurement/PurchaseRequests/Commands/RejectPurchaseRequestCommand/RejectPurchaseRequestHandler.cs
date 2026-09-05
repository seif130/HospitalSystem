using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.RejectPurchaseRequestCommand
{
    public sealed class RejectPurchaseRequestHandler(
        IPurchaseRequestRepository requests,IUnitOfWork unitOfWork)
        : ICommandHandler<RejectPurchaseRequestCommand>
    {
        public async Task<Result> Handle(RejectPurchaseRequestCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseRequest = await requests.GetByIdAsync(
                request.PurchaseRequestId,
                cancellationToken);

            if (purchaseRequest is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "PurchaseRequest.NotFound",
                        "Purchase request was not found."));
            }

            purchaseRequest.Reject(request.Reason);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
