using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.UpdateDepartmentCommand
{

    public sealed class UpdateDepartmentDetailsCommandValidator : AbstractValidator<UpdateDepartmentDetailsCommand>
    {
        public UpdateDepartmentDetailsCommandValidator()
        {
            RuleFor(x => x.DepartmentId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Description).MaximumLength(1000).When(x => x.Description is not null);
        }
    }
}
