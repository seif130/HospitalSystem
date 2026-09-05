using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.CreatePurchaseOrderCommand
{
    public sealed record CreatePurchaseOrderCommand(VendorId VendorId, string Currency, PurchaseRequestId? PurchaseRequestId) : ICommand<PurchaseOrderId>;

}
