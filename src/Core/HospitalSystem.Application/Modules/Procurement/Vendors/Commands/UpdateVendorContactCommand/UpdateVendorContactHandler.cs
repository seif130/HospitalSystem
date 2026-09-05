using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.UpdateVendorContactCommand
{
    public sealed class UpdateVendorContactHandler(
    IVendorRepository vendors,IUnitOfWork unitOfWork) : ICommandHandler<UpdateVendorContactCommand>
    {
        public async Task<Result> Handle(UpdateVendorContactCommand request,CancellationToken cancellationToken)
        {
            var vendor = await vendors.GetByIdAsync( request.VendorId,cancellationToken);

            if (vendor is null)
            {
                return Result.Failure(
                    Error.NotFound( "Vendor.NotFound","Vendor was not found."));
            }

            vendor.UpdateContact(request.ContactEmail, request.ContactPhone);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
