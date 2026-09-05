using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractsByVendorQuery
{
    public sealed class GetVendorContractsByVendorHandler(IVendorContractRepository contracts)
        : IQueryHandler<GetVendorContractsByVendorQuery, PaginatedList<VendorContractDto>>
    {
        public async Task<Result<PaginatedList<VendorContractDto>>> Handle(
            GetVendorContractsByVendorQuery request,CancellationToken cancellationToken)
        {
            var (contractsList, total) = await contracts.GetByVendorAsync(
                request.VendorId,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var items = contractsList.Select(contract => contract.ToDto()).ToList();

            var result = new PaginatedList<VendorContractDto>(
                items,
                total,
                request.PageNumber,
                request.PageSize);

            return Result.Success(result);
        }
    }
}
