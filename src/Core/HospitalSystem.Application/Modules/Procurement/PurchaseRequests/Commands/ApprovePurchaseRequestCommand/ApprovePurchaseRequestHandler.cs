using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.ApprovePurchaseRequestCommand
{
    public sealed class ApprovePurchaseRequestHandler(IPurchaseRequestRepository requests,IUnitOfWork unitOfWork)
        : ICommandHandler<ApprovePurchaseRequestCommand>
    {
        public async Task<Result> Handle(ApprovePurchaseRequestCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseRequest = await requests.GetByIdAsync(
                request.PurchaseRequestId,
                cancellationToken);

            if (purchaseRequest is null)
            {
                return Result.Failure(Error.NotFound("PurchaseRequest.NotFound",
                        "Purchase request was not found."));
            }

            purchaseRequest.Approve();

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
