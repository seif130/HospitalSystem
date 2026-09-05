using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.CancelPurchaseRequestCommand
{
    public sealed record CancelPurchaseRequestCommand(PurchaseRequestId PurchaseRequestId) : ICommand;

}
