using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.ApprovePurchaseOrderCommand
{
    public sealed record ApprovePurchaseOrderCommand(PurchaseOrderId PurchaseOrderId) : ICommand;

}
