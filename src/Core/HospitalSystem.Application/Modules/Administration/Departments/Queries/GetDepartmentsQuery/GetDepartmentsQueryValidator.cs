using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Queries.GetDepartmentsQuery
{
    public class GetDepartmentsQueryValidator : AbstractValidator<GetDepartmentsQuery>
    {
        public GetDepartmentsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page must be greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");
        }
    }
}
