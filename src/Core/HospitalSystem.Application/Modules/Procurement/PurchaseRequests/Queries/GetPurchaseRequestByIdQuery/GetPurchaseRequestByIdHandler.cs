using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestByIdQuery
{
    public sealed class GetPurchaseRequestByIdHandler(IPurchaseRequestRepository requests)
        : IQueryHandler<GetPurchaseRequestByIdQuery, PurchaseRequestDto>
    {
        public async Task<Result<PurchaseRequestDto>> Handle(
            GetPurchaseRequestByIdQuery request,CancellationToken cancellationToken)
        {
            var purchaseRequest = await requests.GetByIdAsync(
                request.PurchaseRequestId,
                cancellationToken);

            if (purchaseRequest is null)
            {
                return Result.Failure<PurchaseRequestDto>(
                    Error.NotFound("PurchaseRequest.NotFound",
                        "Purchase request was not found."));
            }

            return Result.Success(purchaseRequest.ToDto());
        }
    }
}
