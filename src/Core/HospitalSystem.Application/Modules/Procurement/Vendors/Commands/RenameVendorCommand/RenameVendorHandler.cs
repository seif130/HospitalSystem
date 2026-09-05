using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand
{
    public sealed class RenameVendorHandler(
      IVendorRepository vendors,IUnitOfWork unitOfWork): ICommandHandler<RenameVendorCommand>
    {
        public async Task<Result> Handle(RenameVendorCommand request,
            CancellationToken cancellationToken)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId,cancellationToken);

            if (vendor is null)
            {
                return Result.Failure(
                    Error.NotFound("Vendor.NotFound","Vendor was not found."));
            }

            var name = request.Name.Trim();
            var normalizedName = name.ToUpperInvariant();

            if (!string.Equals(vendor.Name, name,
                    StringComparison.OrdinalIgnoreCase)
                && await vendors.ExistsByNormalizedNameAsync(
                    normalizedName,request.VendorId,cancellationToken))
            {
                return Result.Failure(
                    Error.Conflict("Vendor.DuplicateName",
                        "A vendor with this name already exists."));
            }

            vendor.Rename(name);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
