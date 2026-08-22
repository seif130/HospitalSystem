using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorsByDepartmentQuery
{
    public sealed class GetDoctorsByDepartmentQueryValidator
       : AbstractValidator<GetDoctorsByDepartmentQuery>
    {
        public GetDoctorsByDepartmentQueryValidator()
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty();
        }
    }

}
