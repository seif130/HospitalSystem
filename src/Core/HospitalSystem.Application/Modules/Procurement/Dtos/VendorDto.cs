using HospitalSystem.Domain.Modules.Procurement.Vendors.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Dtos
{
    public sealed record VendorDto(
        Guid Id, string Name, string? ContactEmail, string? ContactPhone, VendorStatus Status);


}
