using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.DeactivateVendorCommand
{
    public sealed class DeactivateVendorHandler(
     IVendorRepository vendors,IUnitOfWork unitOfWork): ICommandHandler<DeactivateVendorCommand>
    {
        public async Task<Result> Handle(
            DeactivateVendorCommand request,
            CancellationToken cancellationToken)
        {
            var vendor = await vendors.GetByIdAsync(
                request.VendorId,
                cancellationToken);

            if (vendor is null)
            {
                return Result.Failure(
                    Error.NotFound("Vendor.NotFound","Vendor was not found."));
            }

            vendor.Deactivate();

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }

}
