using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.ActivateVendorCommand
{
    public sealed record ActivateVendorCommand(VendorId VendorId) : ICommand;

}
