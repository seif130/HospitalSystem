using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorSpecialtyCommand
{
    public sealed class ChangeDoctorSpecialtyCommandValidator
        : AbstractValidator<ChangeDoctorSpecialtyCommand>
    {
        public ChangeDoctorSpecialtyCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.Specialty)
                .IsInEnum();
        }
    }

}
