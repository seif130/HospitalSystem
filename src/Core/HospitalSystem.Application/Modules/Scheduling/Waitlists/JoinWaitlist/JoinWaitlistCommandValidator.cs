using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.JoinWaitlist
{
    public sealed class JoinWaitlistCommandValidator : AbstractValidator<JoinWaitlistCommand>
    {
        public JoinWaitlistCommandValidator()
        {
            RuleFor(c => c.PatientId).NotEmpty();
            RuleFor(c => c.DoctorId).NotEmpty();
            RuleFor(c => c.PreferredFromUtc).GreaterThan(DateTime.UtcNow);
            RuleFor(c => c.PreferredToUtc).GreaterThan(c => c.PreferredFromUtc);
        }
    }
}
