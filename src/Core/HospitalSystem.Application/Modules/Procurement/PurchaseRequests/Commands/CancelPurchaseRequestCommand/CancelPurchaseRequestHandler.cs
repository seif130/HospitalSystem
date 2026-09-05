using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.CancelPurchaseRequestCommand
{
    public sealed class CancelPurchaseRequestHandler(IPurchaseRequestRepository requests,IUnitOfWork unitOfWork)
        : ICommandHandler<CancelPurchaseRequestCommand>
    {
        public async Task<Result> Handle(CancelPurchaseRequestCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseRequest = await requests.GetByIdAsync(request.PurchaseRequestId,cancellationToken);

            if (purchaseRequest is null)
            {
                return Result.Failure(Error.NotFound("PurchaseRequest.NotFound",
                        "Purchase request was not found."));
            }

            purchaseRequest.Cancel();

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
