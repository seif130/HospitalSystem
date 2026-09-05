using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand
{
    public sealed record CreateVendorCommand(string Name, string? ContactEmail, string? ContactPhone) : ICommand<VendorId>;

}
