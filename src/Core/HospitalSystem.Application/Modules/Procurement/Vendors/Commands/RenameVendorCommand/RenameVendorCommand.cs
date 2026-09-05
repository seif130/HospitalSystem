using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand
{
    public sealed record RenameVendorCommand(VendorId VendorId, string Name) : ICommand;

}
