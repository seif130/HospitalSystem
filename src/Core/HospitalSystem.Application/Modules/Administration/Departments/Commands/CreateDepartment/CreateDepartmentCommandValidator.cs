using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required.")
                .MaximumLength(150).WithMessage("Department name must not exceed 150 characters.");

            RuleFor(x => x.Description).MaximumLength(1000).When(x => x.Description is not null)
                .WithMessage("Description must not exceed 1000 characters.");
        }
    }
}
