using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.RenameSpecialty
{
    public sealed class RenameSpecialtyCommandValidator
           : AbstractValidator<RenameSpecialtyCommand>
    {
        public RenameSpecialtyCommandValidator()
        {
            RuleFor(x => x.SpecialtyId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}
