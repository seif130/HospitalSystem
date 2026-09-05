using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Queries.GetVendorByIdQuery
{
    public sealed class GetVendorByIdHandler(IVendorRepository vendors)
     : IQueryHandler<GetVendorByIdQuery, VendorDto>
    {
        public async Task<Result<VendorDto>> Handle(GetVendorByIdQuery request,CancellationToken cancellationToken)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId,cancellationToken);

            if (vendor is null)
            {
                return Result.Failure<VendorDto>(
                    Error.NotFound("Vendor.NotFound","Vendor was not found."));
            }

            return Result.Success(vendor.ToDto());
        }
    }

}
