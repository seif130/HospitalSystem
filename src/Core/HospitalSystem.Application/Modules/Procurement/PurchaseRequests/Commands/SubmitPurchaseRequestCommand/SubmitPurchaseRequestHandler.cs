using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.SubmitPurchaseRequestCommand
{
    public sealed class SubmitPurchaseRequestHandler(
        IPurchaseRequestRepository requests,IUnitOfWork unitOfWork): ICommandHandler<SubmitPurchaseRequestCommand>
    {
        public async Task<Result> Handle( SubmitPurchaseRequestCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseRequest = await requests.GetByIdAsync(request.PurchaseRequestId,cancellationToken);

            if (purchaseRequest is null)
            {
                return Result.Failure(
                    Error.NotFound("PurchaseRequest.NotFound",
                        "Purchase request was not found."));
            }

            purchaseRequest.Submit();

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
