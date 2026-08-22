using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Queries.GetDepartmentById
{
    public sealed class GetDepartmentByIdQueryValidator
        : AbstractValidator<GetDepartmentByIdQuery>
    {
        public GetDepartmentByIdQueryValidator()
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty();
        }
    }

}
