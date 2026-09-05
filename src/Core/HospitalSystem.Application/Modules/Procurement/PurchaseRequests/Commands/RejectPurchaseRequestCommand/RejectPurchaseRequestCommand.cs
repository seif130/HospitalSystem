using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.RejectPurchaseRequestCommand
{
    public sealed record RejectPurchaseRequestCommand(PurchaseRequestId PurchaseRequestId, string Reason) : ICommand;

}
