using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand
{
    public sealed record CreateVendorContractCommand(
        VendorId VendorId, VendorServiceCategory Category,
        DateTime Start, DateTime End, decimal Amount, string Currency) : ICommand<VendorContractId>;

}
