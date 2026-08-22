using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.CreateSpecialty
{
    public sealed class CreateSpecialtyCommandValidator
           : AbstractValidator<CreateSpecialtyCommand>
    {
        public CreateSpecialtyCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
