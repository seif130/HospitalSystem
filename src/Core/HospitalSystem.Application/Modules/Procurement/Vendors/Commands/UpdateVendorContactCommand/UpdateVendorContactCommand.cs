using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.UpdateVendorContactCommand
{
    public sealed record UpdateVendorContactCommand(VendorId VendorId, string? ContactEmail, string? ContactPhone) : ICommand;

}
