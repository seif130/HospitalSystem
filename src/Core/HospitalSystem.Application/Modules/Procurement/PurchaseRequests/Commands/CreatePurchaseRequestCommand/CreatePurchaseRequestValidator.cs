using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.CreatePurchaseRequestCommand
{
    public sealed class CreatePurchaseRequestValidator : AbstractValidator<CreatePurchaseRequestCommand>
    {
        public CreatePurchaseRequestValidator()
        { 
            RuleFor(x => x.DepartmentId.Value).NotEmpty(); 
            RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000);
        }
    }

}
