using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitingByDoctorQuery
{
    public sealed class GetWaitingByDoctorQueryValidator
        : AbstractValidator<GetWaitingByDoctorQuery>
    {
        public GetWaitingByDoctorQueryValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.FromUtc)
                .NotEmpty();

            RuleFor(x => x.ToUtc)
                .GreaterThan(x => x.FromUtc);
        }
    }

}
