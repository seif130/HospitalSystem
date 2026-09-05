using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Queries.GetVendorByIdQuery
{
    public sealed record GetVendorByIdQuery(VendorId VendorId) : IQuery<VendorDto>;

}
