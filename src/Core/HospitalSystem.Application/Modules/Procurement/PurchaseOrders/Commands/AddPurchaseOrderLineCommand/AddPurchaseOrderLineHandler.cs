using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.AddPurchaseOrderLineCommand
{
    public sealed class AddPurchaseOrderLineHandler(IPurchaseOrderRepository orders, IUnitOfWork unitOfWork)
        : ICommandHandler<AddPurchaseOrderLineCommand>
    {
        public async Task<Result> Handle(AddPurchaseOrderLineCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseOrder = await orders.GetByIdAsync( request.PurchaseOrderId, cancellationToken);

            if (purchaseOrder is null)
            {
                return Result.Failure(Error.NotFound("PurchaseOrder.NotFound",
                        "Purchase order was not found."));
            }

            purchaseOrder.AddLine(request.ItemName, request.Quantity,
                Money.Create(request.UnitPrice,request.Currency));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
