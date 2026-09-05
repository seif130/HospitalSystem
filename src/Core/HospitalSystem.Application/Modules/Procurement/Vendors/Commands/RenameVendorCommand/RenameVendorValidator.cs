using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand
{
    public sealed class RenameVendorValidator : AbstractValidator<RenameVendorCommand> 
    { 
        public RenameVendorValidator()
        {
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        } 
    }

}
