using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorDepartmentCommand
{
    public sealed class ChangeDoctorDepartmentCommandValidator
        : AbstractValidator<ChangeDoctorDepartmentCommand>
    {
        public ChangeDoctorDepartmentCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.DepartmentId)
                .NotEmpty();
        }
    }

}
