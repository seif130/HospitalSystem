using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.JoinWaitlistCommand
{
    public sealed class JoinWaitlistCommandValidator
     : AbstractValidator<JoinWaitlistCommand>
    {
        public JoinWaitlistCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.PreferredFromUtc)
                .NotEmpty();

            RuleFor(x => x.PreferredToUtc)
                .NotEmpty()
                .GreaterThan(x => x.PreferredFromUtc);
        }
    }


}
