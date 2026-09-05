using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractByIdQuery
{
    public sealed class GetVendorContractByIdHandler(IVendorContractRepository contracts)
      : IQueryHandler<GetVendorContractByIdQuery, VendorContractDto>
    {
        public async Task<Result<VendorContractDto>> Handle(
            GetVendorContractByIdQuery request,CancellationToken cancellationToken)
        {
            var contract = await contracts.GetByIdAsync(
                request.VendorContractId,cancellationToken);

            if (contract is null)
            {
                return Result.Failure<VendorContractDto>(
                    Error.NotFound("VendorContract.NotFound",
                        "Vendor contract was not found."));
            }

            return Result.Success(contract.ToDto());
        }
    }
}
