using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorByIdQuery
{
    public sealed class GetDoctorByIdQueryValidator
        : AbstractValidator<GetDoctorByIdQuery>
    {
        public GetDoctorByIdQueryValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty();
        }
    }

}
