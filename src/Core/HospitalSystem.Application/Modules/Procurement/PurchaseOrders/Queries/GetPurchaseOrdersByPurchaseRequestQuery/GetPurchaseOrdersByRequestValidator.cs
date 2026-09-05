using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByPurchaseRequestQuery
{
    public sealed class GetPurchaseOrdersByRequestValidator : AbstractValidator<GetPurchaseOrdersByPurchaseRequestQuery> 
    {
        public GetPurchaseOrdersByRequestValidator()
        { 
            RuleFor(x => x.PurchaseRequestId.Value).NotEmpty();
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100); 
        }
    }

}
