using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.AddPurchaseRequestLineCommand
{
    public sealed class AddPurchaseRequestLineHandler(
        IPurchaseRequestRepository requests,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddPurchaseRequestLineCommand>
    {
        public async Task<Result> Handle(
            AddPurchaseRequestLineCommand request,
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

            purchaseRequest.AddLine(
                request.ItemName,
                request.Quantity,
                Money.Create(
                    request.EstimatedUnitPrice,
                    request.Currency));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
