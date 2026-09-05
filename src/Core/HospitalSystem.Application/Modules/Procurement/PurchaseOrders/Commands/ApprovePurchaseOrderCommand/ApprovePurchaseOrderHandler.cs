using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.ApprovePurchaseOrderCommand
{
    public sealed class ApprovePurchaseOrderHandler(
        IPurchaseOrderRepository orders,IUnitOfWork unitOfWork)
        : ICommandHandler<ApprovePurchaseOrderCommand>
    {
        public async Task<Result> Handle(ApprovePurchaseOrderCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseOrder = await orders.GetByIdAsync(
                request.PurchaseOrderId,cancellationToken);

            if (purchaseOrder is null)
            {
                return Result.Failure(Error.NotFound("PurchaseOrder.NotFound",
                        "Purchase order was not found."));
            }

            purchaseOrder.Approve();

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
