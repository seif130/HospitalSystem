using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorsBySpecialtyQuery
{
    public sealed class GetDoctorsBySpecialtyQueryValidator
        : AbstractValidator<GetDoctorsBySpecialtyQuery>
    {
        public GetDoctorsBySpecialtyQueryValidator()
        {
            RuleFor(x => x.Specialty)
                .IsInEnum();
        }
    }

}
