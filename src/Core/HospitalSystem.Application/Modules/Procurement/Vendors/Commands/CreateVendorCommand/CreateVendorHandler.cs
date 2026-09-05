using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Vendors;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand
{
    public sealed class CreateVendorHandler(
      IVendorRepository vendors,IUnitOfWork unitOfWork) : ICommandHandler<CreateVendorCommand, VendorId>
    {
        public async Task<Result<VendorId>> Handle(
            CreateVendorCommand request,
            CancellationToken cancellationToken)
        {
            var name = request.Name.Trim();
            var normalizedName = name.ToUpperInvariant();

            var exists = await vendors.ExistsByNormalizedNameAsync(normalizedName, cancellationToken: cancellationToken);

            if (exists)
            {
                return Result.Failure<VendorId>(Error.Conflict("Vendor.AlreadyExists",
                        "A vendor with this name already exists."));
            }

            var vendor = Vendor.Create(name,request.ContactEmail, request.ContactPhone);

            await vendors.AddAsync(
                vendor,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(vendor.Id);
        }
    }

}
