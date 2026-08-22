using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.UpdateSpecialty
{
    public sealed class UpdateSpecialtyDescriptionCommandValidator
           : AbstractValidator<UpdateSpecialtyDescriptionCommand>
    {
        public UpdateSpecialtyDescriptionCommandValidator()
        {
            RuleFor(x => x.SpecialtyId)
                .NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
