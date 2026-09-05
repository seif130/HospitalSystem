using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.SubmitPurchaseOrderCommand
{
    public sealed class SubmitPurchaseOrderHandler(IPurchaseOrderRepository orders,IUnitOfWork unitOfWork)
        : ICommandHandler<SubmitPurchaseOrderCommand>
    {
        public async Task<Result> Handle(
            SubmitPurchaseOrderCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseOrder = await orders.GetByIdAsync(request.PurchaseOrderId,cancellationToken);

            if (purchaseOrder is null)
            {
                return Result.Failure(
                    Error.NotFound("PurchaseOrder.NotFound",
                        "Purchase order was not found."));
            }

            purchaseOrder.Submit();

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
