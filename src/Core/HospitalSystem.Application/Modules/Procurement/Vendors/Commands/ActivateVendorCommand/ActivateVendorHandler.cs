using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.ActivateVendorCommand
{
    public sealed class ActivateVendorHandler(IVendorRepository vendors,IUnitOfWork unitOfWork): ICommandHandler<ActivateVendorCommand>
    {
        public async Task<Result> Handle(ActivateVendorCommand request,CancellationToken cancellationToken)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId,cancellationToken);

            if (vendor is null)
            {
                return Result.Failure(Error.NotFound("Vendor.NotFound","Vendor was not found."));
            }

            vendor.Activate();

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
