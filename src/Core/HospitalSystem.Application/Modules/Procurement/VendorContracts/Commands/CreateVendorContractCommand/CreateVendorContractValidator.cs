using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand
{
    public sealed class CreateVendorContractValidator : AbstractValidator<CreateVendorContractCommand>
    { 
        public CreateVendorContractValidator()
        {
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.Category).IsInEnum();
            RuleFor(x => x.End).GreaterThan(x => x.Start); 
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty().Length(3);
        }
    }

}
