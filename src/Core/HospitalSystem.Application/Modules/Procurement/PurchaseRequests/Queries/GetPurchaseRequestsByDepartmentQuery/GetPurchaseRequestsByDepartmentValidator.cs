using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestsByDepartmentQuery
{
    public sealed class GetPurchaseRequestsByDepartmentValidator : AbstractValidator<GetPurchaseRequestsByDepartmentQuery>
    { 
        public GetPurchaseRequestsByDepartmentValidator()
        {
            RuleFor(x => x.DepartmentId.Value).NotEmpty();
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100); 
        }
    }

}
