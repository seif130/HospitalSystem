using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitlistEntryByIdQuery
{
    public sealed class GetWaitlistByIdQueryValidator
        : AbstractValidator<GetWaitlistByIdQuery>
    {
        public GetWaitlistByIdQueryValidator()
        {
            RuleFor(x => x.WaitlistId)
                .NotEmpty();
        }
    }

}
