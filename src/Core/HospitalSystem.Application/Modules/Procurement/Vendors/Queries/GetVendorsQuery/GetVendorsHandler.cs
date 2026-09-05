using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Queries.GetVendorsQuery
{
    public sealed class GetVendorsHandler(
    IVendorRepository vendors): IQueryHandler<GetVendorsQuery, PaginatedList<VendorDto>>
    {
        public async Task<Result<PaginatedList<VendorDto>>> Handle(
            GetVendorsQuery request,
            CancellationToken cancellationToken)
        {
            var (vendorsList, total) = await vendors.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var items = vendorsList
                .Select(v => v.ToDto())
                .ToList();

            var result = new PaginatedList<VendorDto>(
                items,
                total,
                request.PageNumber,
                request.PageSize);

            return Result.Success(result);
        }
    }

}
