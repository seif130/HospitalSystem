using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.AddPurchaseOrderLineCommand
{
    public sealed record AddPurchaseOrderLineCommand(PurchaseOrderId PurchaseOrderId, string ItemName, int Quantity, decimal UnitPrice, string Currency) : ICommand;

}
