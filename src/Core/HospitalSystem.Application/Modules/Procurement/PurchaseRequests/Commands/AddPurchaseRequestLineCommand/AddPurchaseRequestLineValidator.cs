using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.AddPurchaseRequestLineCommand
{
    public sealed class AddPurchaseRequestLineValidator : AbstractValidator<AddPurchaseRequestLineCommand> 
    {
        public AddPurchaseRequestLineValidator()
        {
            RuleFor(x => x.PurchaseRequestId.Value).NotEmpty();
            RuleFor(x => x.ItemName).NotEmpty().MaximumLength(300); 
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.EstimatedUnitPrice).GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty().Length(3);
        }
    }

}
