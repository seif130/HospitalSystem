using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.CreatePurchaseRequestCommand
{
    public sealed record CreatePurchaseRequestCommand(DepartmentId DepartmentId, string Reason) : ICommand<PurchaseRequestId>;

}
