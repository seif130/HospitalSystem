using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.TerminateVendorContractCommand
{
    public sealed class TerminateVendorContractValidator : AbstractValidator<TerminateVendorContractCommand>
    { 
        public TerminateVendorContractValidator() 
        {
            RuleFor(x => x.VendorContractId.Value).NotEmpty(); 
            RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000); 
        }
    }

}
