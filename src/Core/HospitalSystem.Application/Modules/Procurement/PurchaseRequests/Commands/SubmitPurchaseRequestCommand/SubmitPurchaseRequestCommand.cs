using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.SubmitPurchaseRequestCommand
{
    public sealed record SubmitPurchaseRequestCommand(PurchaseRequestId PurchaseRequestId) : ICommand;

}
