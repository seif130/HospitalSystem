using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Contract;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Enum;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand
{
    public sealed class CreateVendorContractHandler(
       IVendorRepository vendors,IVendorContractRepository contracts,IUnitOfWork unitOfWork)
       : ICommandHandler<CreateVendorContractCommand, VendorContractId>
    {
        public async Task<Result<VendorContractId>> Handle(
            CreateVendorContractCommand request,
            CancellationToken cancellationToken)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId,
                cancellationToken);

            if (vendor is null)
            {
                return Result.Failure<VendorContractId>(
                    Error.NotFound("Vendor.NotFound",
                        "Vendor was not found."));
            }

            if (vendor.Status != VendorStatus.Active)
            {
                return Result.Failure<VendorContractId>(
                    Error.Conflict(
                        "Vendor.Inactive",
                        "Only active vendors can have contracts."));
            }

            var term = DateRange.Create(
                request.Start,
                request.End);

            var value = Money.Create(
                request.Amount,
                request.Currency);

            var contract = VendorContract.Draft(
                request.VendorId,
                request.Category,
                term,
                value);

            await contracts.AddAsync(contract,cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(contract.Id);
        }
    }
}
