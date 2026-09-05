using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.AddPurchaseOrderLineCommand
{
    public sealed class AddPurchaseOrderLineValidator : AbstractValidator<AddPurchaseOrderLineCommand> 
    {
        public AddPurchaseOrderLineValidator() 
        { 
            RuleFor(x => x.PurchaseOrderId.Value).NotEmpty();
            RuleFor(x => x.ItemName).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Quantity).GreaterThan(0); RuleFor(x => x.UnitPrice).GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty().Length(3); 
        }
    }

}
