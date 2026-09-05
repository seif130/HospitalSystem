using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.CancelPurchaseOrderCommand
{
    public sealed class CancelPurchaseOrderHandler(
        IPurchaseOrderRepository orders,IUnitOfWork unitOfWork)
        : ICommandHandler<CancelPurchaseOrderCommand>
    {
        public async Task<Result> Handle(
            CancelPurchaseOrderCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseOrder = await orders.GetByIdAsync(
                request.PurchaseOrderId,
                cancellationToken);

            if (purchaseOrder is null)
            {
                return Result.Failure(
                    Error.NotFound("PurchaseOrder.NotFound",
                        "Purchase order was not found."));
            }

            purchaseOrder.Cancel();

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
