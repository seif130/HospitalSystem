using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.RejectPurchaseRequestCommand
{
    public sealed class RejectPurchaseRequestValidator : AbstractValidator<RejectPurchaseRequestCommand> 
    {
        public RejectPurchaseRequestValidator() 
        {
            RuleFor(x => x.PurchaseRequestId.Value).NotEmpty(); 
            RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000);
        }
    }

}
