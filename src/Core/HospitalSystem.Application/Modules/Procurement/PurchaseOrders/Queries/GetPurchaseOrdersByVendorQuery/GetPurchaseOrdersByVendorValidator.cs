using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByVendorQuery
{
    public sealed class GetPurchaseOrdersByVendorValidator : AbstractValidator<GetPurchaseOrdersByVendorQuery> 
    {
        public GetPurchaseOrdersByVendorValidator() {
            RuleFor(x => x.VendorId.Value).NotEmpty();
            RuleFor(x => x.PageNumber).GreaterThan(0); 
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100); 
        }
    }

}
