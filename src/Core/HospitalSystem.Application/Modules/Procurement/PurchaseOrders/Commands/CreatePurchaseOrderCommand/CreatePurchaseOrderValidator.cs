using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.CreatePurchaseOrderCommand
{
    public sealed class CreatePurchaseOrderValidator : AbstractValidator<CreatePurchaseOrderCommand> 
    { 
        public CreatePurchaseOrderValidator()
        {
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.Currency).NotEmpty().Length(3);
        }
    }

}
