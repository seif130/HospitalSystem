using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand
{
    public sealed class CreateVendorValidator : AbstractValidator<CreateVendorCommand>
    {
        public CreateVendorValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.ContactEmail).MaximumLength(320).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.ContactEmail));
            RuleFor(x => x.ContactPhone).MaximumLength(50);
        }
    }
}
