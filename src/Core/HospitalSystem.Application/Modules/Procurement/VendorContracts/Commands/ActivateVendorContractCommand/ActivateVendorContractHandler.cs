using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.ActivateVendorContractCommand
{
    public sealed class ActivateVendorContractHandler(
        IVendorContractRepository contracts,IUnitOfWork unitOfWork)
        : ICommandHandler<ActivateVendorContractCommand>
    {
        public async Task<Result> Handle(ActivateVendorContractCommand request,
            CancellationToken cancellationToken)
        {
            var contract = await contracts.GetByIdAsync(
                request.VendorContractId,
                cancellationToken);

            if (contract is null)
            {
                return Result.Failure(
                    Error.NotFound("VendorContract.NotFound",
                        "Vendor contract was not found."));
            }

            contract.Activate();

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
