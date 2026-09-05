using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.TerminateVendorContractCommand
{
    public sealed record TerminateVendorContractCommand(VendorContractId VendorContractId, string Reason) : ICommand;

}
