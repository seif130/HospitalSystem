using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.UpdateVendorContactCommand
{
    public sealed class UpdateVendorContactValidator : AbstractValidator<UpdateVendorContactCommand> 
    {
        public UpdateVendorContactValidator()
        {
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.ContactEmail).MaximumLength(320).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.ContactEmail)); 
            RuleFor(x => x.ContactPhone).MaximumLength(50);
        } 
    }

}
