using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.CompletePurchaseOrderCommand
{
    public sealed record CompletePurchaseOrderCommand(PurchaseOrderId PurchaseOrderId) : ICommand;

}
