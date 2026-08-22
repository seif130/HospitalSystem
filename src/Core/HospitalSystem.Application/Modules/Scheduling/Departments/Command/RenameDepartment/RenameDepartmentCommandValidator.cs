using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Command.RenameDepartment
{
    public sealed class RenameDepartmentCommandValidator
        : AbstractValidator<RenameDepartmentCommand>
    {
        public RenameDepartmentCommandValidator()
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
        }
    }

}
