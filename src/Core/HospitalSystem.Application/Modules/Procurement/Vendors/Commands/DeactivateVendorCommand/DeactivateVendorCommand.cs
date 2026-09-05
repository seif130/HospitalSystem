using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.DeactivateVendorCommand
{
    public sealed record DeactivateVendorCommand(VendorId VendorId) : ICommand;

}
