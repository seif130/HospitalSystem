using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.CreatePurchaseRequestCommand
{
    public sealed class CreatePurchaseRequestHandler(
        IPurchaseRequestRepository requests,IUnitOfWork unitOfWork)
        : ICommandHandler<CreatePurchaseRequestCommand, PurchaseRequestId>
    {
        public async Task<Result<PurchaseRequestId>> Handle(
            CreatePurchaseRequestCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseRequest = PurchaseRequest.Create(
                request.DepartmentId,request.Reason);

            await requests.AddAsync(purchaseRequest,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(purchaseRequest.Id);
        }
    }
}
