using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Vendors.Queries.GetVendorsQuery
{
    public sealed class GetVendorsValidator : AbstractValidator<GetVendorsQuery> 
    { 
        public GetVendorsValidator() 
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100); 
        }
    }

}
