using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.CancelWaitlistCommand
{
    public sealed class CancelWaitlistCommandValidator
        : AbstractValidator<CancelWaitlistCommand>
    {
        public CancelWaitlistCommandValidator()
        {
            RuleFor(x => x.WaitlistId)
                .NotEmpty();
        }
    }

}
