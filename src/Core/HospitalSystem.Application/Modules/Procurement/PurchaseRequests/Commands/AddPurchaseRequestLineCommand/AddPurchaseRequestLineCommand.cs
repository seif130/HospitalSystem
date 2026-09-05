using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.AddPurchaseRequestLineCommand
{
    public sealed record AddPurchaseRequestLineCommand(PurchaseRequestId PurchaseRequestId, string ItemName,
        int Quantity, decimal EstimatedUnitPrice, string Currency) : ICommand;

}
