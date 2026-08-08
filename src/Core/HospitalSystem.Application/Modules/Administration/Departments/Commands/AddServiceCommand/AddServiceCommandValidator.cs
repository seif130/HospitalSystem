using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddServiceCommand
{
    public sealed class AddServiceCommandValidator : AbstractValidator<AddServiceCommand>
    {
        public AddServiceCommandValidator()
        {
            RuleFor(x => x.DepartmentId).NotEmpty();
            RuleFor(x => x.ServiceName).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Description).MaximumLength(1000).When(x => x.Description is not null);
            RuleFor(x => x.PriceAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Currency).NotEmpty().Length(3);
        }
    }
}
